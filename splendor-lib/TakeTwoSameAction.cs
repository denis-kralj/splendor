using System;

namespace splendor_lib
{
    public class TakeTwoSameAction : IGameAction
    {
        private TokenCollection _tokensInternal;

        public TakeTwoSameAction(TokenCollection tokensToTake)
        {
            _tokensInternal = tokensToTake;
        }
        public bool TryExecuteAction(Player player, GameBoard board, out ExecutionResult result)
        {
            if (InvalidTokenCombination())
            {
                result = ExecutionResult.InvalidTokenCombination;
                return false;
            }

            if(BoardDoesntHaveAtLeastFour(board))
            {
                result = ExecutionResult.InsufficientTokens;
                return false;
            }
            
            var success = board.TryTakeTokens(_tokensInternal);
            result = success ? ExecutionResult.Success : ExecutionResult.InsufficientTokens;
            return success;
        }

        private bool BoardDoesntHaveAtLeastFour(GameBoard board)
        {
            foreach(Token type in Enum.GetValues(typeof(Token)))
            {
                if(_tokensInternal.GetCount(type) == 0)
                    continue;

                if(board.BoardTokens.GetCount(type) < 4)
                    return true;
            }

            return false;
        }

        private bool InvalidTokenCombination()
        {
            if (_tokensInternal.TotalTokens != 2)
                return true;

            foreach (Token key in Enum.GetValues(typeof(Token)))
                if (_tokensInternal.GetCount(key) == 2 || _tokensInternal.GetCount(key) == 0)
                    continue;
                else
                    return true;

            return false;
        }
    }
}