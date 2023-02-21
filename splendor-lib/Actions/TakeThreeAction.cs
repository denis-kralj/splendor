using System.Collections.Generic;
using System.Linq;

namespace splendor_lib;

public class TakeThreeAction : IGameAction
{
    private List<Token> _tokenList;

    private bool IsInvalidTokenCombination => _tokenList.Count > 3 || _tokenList.Distinct().Count() != _tokenList.Count;

    public TakeThreeAction(params Token[] tokens)
    {
        _tokenList = tokens.ToList();
    }

    public bool TryExecuteAction(IPlayer player, IBoard board, out ExecutionResult result)
    {
        if (IsInvalidTokenCombination)
        {
            result = ExecutionResult.InvalidTokenCombination;
            return false;
        }

        var tokenCollection = GetTokenCollection();

        var success = board.TryTakeTokensFormBoard(tokenCollection);
        result = success ? ExecutionResult.Success : ExecutionResult.InsufficientTokens;

        if (success) player.CollectTokens(tokenCollection);

        return success;
    }

    private TokenCollection GetTokenCollection()
    {
        var result = new TokenCollection();

        foreach(var token in _tokenList)
        {
            result.AddTokens(token, 1);
        }

        return result;
    }
}
