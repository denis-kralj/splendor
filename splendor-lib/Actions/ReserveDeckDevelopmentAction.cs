namespace splendor_lib;

public class ReserveDeckDevelopmentAction : IGameAction
{
    private readonly TokenCollection goldPayDay = new TokenCollection(goldCount: 1);
    private readonly DevelopmentDeck _drawLocation;

    public ReserveDeckDevelopmentAction(DevelopmentDeck drawLocation)
    {
        _drawLocation = drawLocation;
    }

    public bool TryExecuteAction(IPlayer player, IBoard board, out ExecutionResult result)
    {
        if (player.HandFull)
        {
            result = ExecutionResult.HandFull;
            return false;
        }

        var success = board.TryTakeDeckDevelopment(_drawLocation, out var development, out result);

        if (success)
        {
            if (board.TryTakeTokensFormBoard(goldPayDay))
            {
                player.CollectTokens(goldPayDay);
            }

            player.TryReserve(development);
        }

        return success;
    }
}
