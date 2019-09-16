using System.Collections.Generic;
using System.Linq;

namespace splendor_lib
{
    public class GameBoard
    {
        private Deck<Development> _lvl1Deck;
        private Deck<Development> _lvl2Deck;
        private Deck<Development> _lvl3Deck;
        private Deck<Noble> _noblesDeck;
        private List<Development> _boardDevelopmentsInternal;
        private List<Noble> _publicNoblesInternal;
        private TokenCollection _boardTokensInternal;
        public List<Noble> BoardNobles => new List<Noble>(_publicNoblesInternal);
        public IReadOnlyTokenCollection BoardTokens => _boardTokensInternal;
        public List<Development> PublicDevelopments => _boardDevelopmentsInternal;
        public GameBoard(PlayerCount playerCount, List<Noble> nobles, List<Development> developments) => SetupBoard(playerCount, nobles, developments);
        public void SetupBoard(PlayerCount playerCount, List<Noble> nobles, List<Development> developments)
        {
            LoadDecks(nobles, developments);
            ShuffleAllDecks();
            DrawInitialBoardDevelopments();
            DrawNobles((uint)playerCount);
            InitTokens(playerCount);
        }
        public void AddTokensToBoard(TokenCollection tokensToReturnToBoard) => _boardTokensInternal.AddTokens(tokensToReturnToBoard);
        public bool TryTakeTokensFormBoard(TokenCollection tokensToGetFromBoard) => _boardTokensInternal.TryTake(tokensToGetFromBoard);
        public bool TryTakeTokensFormBoard(TokenColor tokenColor, uint count) => _boardTokensInternal.TryTake(tokenColor, count);
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
        private bool TakeFromDeck(Deck<Development> deck, out Development actuallyTaken)
        {
            actuallyTaken = deck.IsEmpty ? null : deck.Draw().First();

            return actuallyTaken != null;
        }
        private bool TakeFromPublic(Development developmentToTake, out Development actuallyTaken)
        {
            if(PublicDevelopments.Contains(developmentToTake))
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
                case PlayerCount.Two: default:
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
        private void DrawNobles(uint playerCount) => _publicNoblesInternal = _noblesDeck.Draw(playerCount + 1);
    }
}