using splendor_lib;

namespace splendor_tests;

public static class BoardExtensions
{
    public static void RemoveAllTokensOfType(this IBoard board, Token type)
    {
        board.RemoveToken(type, board.GetTokenCount(type));
    }

    public static void RemoveAllTokens(this IBoard board)
    {
        Tokens.AllTokens.ForEach(t => board.RemoveAllTokensOfType(t));
    }
}