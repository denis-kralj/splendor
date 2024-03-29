using System.Collections.Generic;
using System.Linq;

namespace splendor_lib;


public class GameBoard : IBoard
{
    private IDeck<Development> _lvl1Deck;
    private IDeck<Development> _lvl2Deck;
    private IDeck<Development> _lvl3Deck;
    private IDeck<Noble> _noblesDeck;
    private List<Development> _boardDevelopmentsInternal;
    private List<Noble> _publicNoblesInternal;
    private TokenCollection _boardTokensInternal;

    public GameBoard(PlayerCount playerCount, List<Noble> allNobles, List<Development> allDevelopments)
    {
        InitTokens(playerCount);
        LoadDecks(allNobles, allDevelopments);
        ShuffleAllDecks();
        DrawInitialBoardDevelopments();
        DrawNobles((uint)playerCount);
    }

    public List<Noble> BoardNobles => new List<Noble>(_publicNoblesInternal);
    public IReadOnlyTokenCollection BoardTokens => _boardTokensInternal;
    public List<Development> PublicDevelopments => _boardDevelopmentsInternal;

    public bool TryRemoveDevelopment(Location location, Development developmentToTake, out Development actuallyTaken)
    {
        switch (location)
        {
            case Location.Public: default: return TakeFromPublic(developmentToTake, out actuallyTaken);
            case Location.Level1Deck: return TakeFromDeck(_lvl1Deck, out actuallyTaken);
            case Location.Level2Deck: return TakeFromDeck(_lvl2Deck, out actuallyTaken);
            case Location.Level3Deck: return TakeFromDeck(_lvl3Deck, out actuallyTaken);
        }
    }

    private bool TakeFromDeck(IDeck<Development> deck, out Development actuallyTaken)
    {
        actuallyTaken = deck.IsEmpty ? null : deck.Draw().First();

        return (object)actuallyTaken != null;
    }

    private bool TakeFromPublic(Development developmentToTake, out Development actuallyTaken)
    {
        if (PublicDevelopments.Contains(developmentToTake))
        {
            actuallyTaken = developmentToTake;
            _boardDevelopmentsInternal.Remove(developmentToTake);
            return true;
        }

        actuallyTaken = null;
        return false;
    }

    private void InitTokens(PlayerCount playerCount)
    {
        switch (playerCount)
        {
            case PlayerCount.Four:
                _boardTokensInternal = new TokenCollection(7, 7, 7, 7, 7, 5);
                break;
            case PlayerCount.Three:
                _boardTokensInternal = new TokenCollection(5, 5, 5, 5, 5, 5);
                break;
            case PlayerCount.Two:
            default:
                _boardTokensInternal = new TokenCollection(4, 4, 4, 4, 4, 5);
                break;
        }
    }

    private void LoadDecks(List<Noble> nobles, List<Development> developments)
    {
        _lvl1Deck = new Deck<Development>(developments.Where(d => d.Level == 1).ToList());
        _lvl2Deck = new Deck<Development>(developments.Where(d => d.Level == 2).ToList());
        _lvl3Deck = new Deck<Development>(developments.Where(d => d.Level == 3).ToList());
        _noblesDeck = new Deck<Noble>(nobles);
    }

    private void ShuffleAllDecks()
    {
        _lvl1Deck.Shuffle(true);
        _lvl2Deck.Shuffle(true);
        _lvl3Deck.Shuffle(true);
        _noblesDeck.Shuffle(true);
    }

    private void DrawInitialBoardDevelopments()
    {
        uint drawPerDeck = 4;

        _boardDevelopmentsInternal = new List<Development>((int)drawPerDeck * 3);

        _boardDevelopmentsInternal.AddRange(_lvl1Deck.Draw(drawPerDeck));
        _boardDevelopmentsInternal.AddRange(_lvl2Deck.Draw(drawPerDeck));
        _boardDevelopmentsInternal.AddRange(_lvl3Deck.Draw(drawPerDeck));
    }

    private void DrawNobles(uint playerCount)
    {
        _publicNoblesInternal = _noblesDeck.Draw(playerCount + 1);
    }

    public bool TryTakePublicDevelopment(Development developmentToTake, out ExecutionResult executionResult)
    {
        if(!PublicDevelopments.Contains(developmentToTake))
        {
            executionResult = ExecutionResult.InvalidDevelopmentToReserve;
            return false;
        }

        TakeFromPublic(developmentToTake, out _);
        executionResult = ExecutionResult.Success;
        return true;
    }

    public bool TryTakeDeckDevelopment(DevelopmentDeck drawLocation, out Development development, out ExecutionResult result)
    {
        IDeck<Development> drawDeck = null;

        switch (drawLocation)
        {
            case DevelopmentDeck.Level1: default: drawDeck = _lvl1Deck; break;
            case DevelopmentDeck.Level2:          drawDeck = _lvl2Deck; break;
            case DevelopmentDeck.Level3:          drawDeck = _lvl3Deck; break;
        }

        var success = TakeFromDeck(drawDeck, out development);

        result = success ? ExecutionResult.Success : ExecutionResult.CantDraw;

        return success;
    }

    public void AddToken(Token type, uint count = 1)
    {
        _boardTokensInternal.AddTokens(type, count);
    }

    public void RemoveToken(Token type, uint count = 1)
    {
        _boardTokensInternal.TryTake(type, count);
    }

    public uint GetTokenCount(Token type)
    {
        return _boardTokensInternal.GetCount(type);
    }
}
