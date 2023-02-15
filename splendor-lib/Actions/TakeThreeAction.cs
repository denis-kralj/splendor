using System.Linq;

namespace splendor_lib;

public class TakeThreeAction : IGameAction
{
    private TokenCollection _tokensInternal;
    private const uint _validTotalCount = 3;
    private const uint _validSingleCount = 1;

    private bool _tokenTotalHigherThenAllowed => _tokensInternal.TotalTokens > _validTotalCount;
    private bool _moreThenOneTokenOfSingleType => Tokens.AllTokens.Any(t => _tokensInternal.GetCount(t) > _validSingleCount);
    private bool IsInvalidTokenCombination => _tokenTotalHigherThenAllowed || _moreThenOneTokenOfSingleType;

    public TakeThreeAction(TokenCollection tokensToTake)
    {
        _tokensInternal = tokensToTake;
    }

    public bool TryExecuteAction(IPlayer player, IBoard board, out ExecutionResult result)
    {
        if (IsInvalidTokenCombination)
        {
            result = ExecutionResult.InvalidTokenCombination;
            return false;
        }

        var success = board.TryTakeTokensFormBoard(_tokensInternal);
        result = success ? ExecutionResult.Success : ExecutionResult.InsufficientTokens;

        if (success) player.CollectTokens(_tokensInternal);

        return success;
    }
}
