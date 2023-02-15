using System.Linq;

namespace splendor_lib;

public class TakeTwoSameAction : IGameAction
{
    private TokenCollection _tokensInternal;
    private const uint _tCount = 2;
    private const uint _minBoardTokenCount = 4;

    public TakeTwoSameAction(TokenCollection tokensToTake)
    {
        _tokensInternal = tokensToTake;
    }

    public bool TryExecuteAction(IPlayer player, IBoard board, out ExecutionResult result)
    {
        if (InvalidTokenCombination())
        {
            result = ExecutionResult.InvalidTokenCombination;
            return false;
        }

        if (BoardHasInsufficientTokens(board))
        {
            result = ExecutionResult.InsufficientTokens;
            return false;
        }

        result = ExecutionResult.Success;
        player.CollectTokens(_tokensInternal);
        return board.TryTakeTokensFormBoard(_tokensInternal);
    }

    private bool BoardHasInsufficientTokens(IBoard board)
    {
        var color = Tokens.AllTokens.First(t => _tokensInternal.GetCount(t) == _tCount);

        return board.BoardTokens.GetCount(color) < _minBoardTokenCount;
    }

    private bool InvalidTokenCombination() =>
        _tokensInternal.TotalTokens != _tCount ||
        !Tokens.AllTokens.Any(t => _tokensInternal.GetCount(t) == _tCount);
}
