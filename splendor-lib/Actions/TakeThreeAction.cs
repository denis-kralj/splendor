using System.Collections.Generic;
using System.Linq;

namespace splendor_lib;

public class TakeThreeAction : IGameAction
{
    private readonly List<Token> _tokenList;

    private bool IsInvalidTokenCombination => _tokenList.Count > 3 || _tokenList.Distinct().Count() != _tokenList.Count;
    private bool BoardHasInsufficientTokens(IBoard board) => _tokenList.Any(t => board.GetTokenCount(t) == 0);

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

        if (BoardHasInsufficientTokens(board))
        {
            result = ExecutionResult.InsufficientTokens;
            return false;
        }

        foreach (var token in _tokenList)
        {
            board.RemoveToken(token);
            player.AddToken(token);
        }

        result = ExecutionResult.Success;
        return true;
    }
}
