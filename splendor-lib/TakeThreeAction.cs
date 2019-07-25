using System;

namespace splendor_lib
{
    public class TakeThreeAction : IGameAction
    {
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
            return success;
        }

        private bool InvalidTokenCombination()
        {
            if(_tokensInternal.TotalTokens > 3)
                return true;

            foreach (Token key in Enum.GetValues(typeof(Token)))
                if(_tokensInternal.GetCount(key) > 1)
                    return true;

            return false;
        }
    }
}