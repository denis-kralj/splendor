namespace splendor_lib;

static class Extensions
{
    public static bool DoesNotContainDevelopment(this IBoard board, Development development)
    {
        return !board.PublicDevelopments.Contains(development);
    }

    public static void TakePublic(this IBoard board, Development toTake)
    {
        board.PublicDevelopments.Remove(toTake);
    }

    public static bool CanNotPayFor(this IPlayer player, Development development)
    {
        return !player.CanPay(development.Cost);
    }

    public static void Gets(this IBoard board, IReadOnlyTokenCollection collected)
    {
        foreach (var type in Tokens.AllTokens)
        {
            board.AddToken(type, collected.GetCount(type));
        }
    }

    public static bool DidNotReserve(this IPlayer player, Development development)
    {
        return !player.ReservedDevelopments.Contains(development);
    }

    public static void RemoveReserver(this IPlayer player, Development development)
    {
        player.ReservedDevelopments.Remove(development);
    }
}