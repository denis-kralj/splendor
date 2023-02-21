namespace splendor_lib;

public class TakeTwoSameAction : IGameAction
{
    private readonly Token _tokenType;
    private const uint _minBoardTokenCount = 4;

    public TakeTwoSameAction(Token type)
    {
        _tokenType = type;
    }

    public bool TryExecuteAction(IPlayer player, IBoard board, out ExecutionResult result)
    {
        if (BoardHasInsufficientTokens(board))
        {
            result = ExecutionResult.InsufficientTokens;
            return false;
        }

        if(_tokenType == Token.Gold)
        {
            result = ExecutionResult.InvalidTokenCombination;
            return false;
        }

        var tokenCollection = GenerateCollection();

        result = ExecutionResult.Success;
        player.CollectTokens(tokenCollection);
        return board.TryTakeTokensFormBoard(tokenCollection);
    }

    private TokenCollection GenerateCollection()
    {
        switch(_tokenType)
        {
            case Token.Onyx: return new TokenCollection(onyxCount: 2);
            case Token.Diamond: return new TokenCollection(diamondCount: 2);
            case Token.Emerald: return new TokenCollection(emeraldCount: 2);
            case Token.Sapphire: return new TokenCollection(sapphireCount: 2);
            case Token.Ruby: default: return new TokenCollection(rubyCount: 2);
        }
    }

    private bool BoardHasInsufficientTokens(IBoard board)
    {
        return board.BoardTokens.GetCount(_tokenType) < _minBoardTokenCount;
    }
}
