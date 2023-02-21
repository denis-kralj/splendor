using splendor_lib;

namespace splendor_tests;

public static class BoardExtensions
{
    public static void RemoveAllTokensOfType(this IBoard board, Token type)
    {
        while(board.GetTokenCount(type) > 0)
        {
            board.RemoveToken(type, board.GetTokenCount(type));
        }
    }

    public static void RemoveAllTokens(this IBoard board)
    {
        foreach(var type in Tokens.AllTokens)
        {
            board.RemoveToken(type, board.GetTokenCount(type));
        }
    }
}