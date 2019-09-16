using System.Linq;

namespace splendor_lib
{
    public class TakeThreeAction : IGameAction
    {
        private const uint _validTotalCount = 3;
        private const uint _validSingleCount = 1;
        private TokenCollection _tokensInternal;

        public TakeThreeAction(TokenCollection tokensToTake)
        {
            _tokensInternal = tokensToTake;
        }
        public bool TryExecuteAction(Player player, GameBoard board, out ExecutionResult result)
        {
            if(InvalidTokenCombination())
            {
                result = ExecutionResult.InvalidTokenCombination;
                return false;
            }

            var success = board.TryTakeTokens(_tokensInternal);
            result = success ? ExecutionResult.Success : ExecutionResult.InsufficientTokens;

            if(success) player.CollectTokens(_tokensInternal);

            return success;
        }
        private bool InvalidTokenCombination() =>
            _tokensInternal.TotalTokens > _validTotalCount ||
            TokenUtils.AllTokens.Any(t => _tokensInternal.GetCount(t) > _validSingleCount);
    }
}