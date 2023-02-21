namespace splendor_lib;

public class TakeTwoSameAction : IGameAction
{
    private readonly Token _tokenType;
    private const uint _minBoardTokenCount = 4;
    private bool BoardHasInsufficientTokens(IBoard board) => board.GetTokenCount(_tokenType) < _minBoardTokenCount;

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

        board.RemoveToken(Token.Gold, 2);
        player.AddToken(Token.Gold, 2);

        result = ExecutionResult.Success;
        return true;
    }
}
