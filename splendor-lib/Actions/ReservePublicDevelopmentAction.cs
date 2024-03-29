namespace splendor_lib;

public class ReservePublicDevelopmentAction : IGameAction
{
    private readonly Development _developmentToReserve;
    private readonly TokenCollection goldPayDay = new TokenCollection(goldCount: 1);

    public ReservePublicDevelopmentAction(Development developmentToReserve)
    {
        _developmentToReserve = developmentToReserve;
    }

    public bool TryExecuteAction(IPlayer player, IBoard board, out ExecutionResult result)
    {
        if (player.HandFull)
        {
            result = ExecutionResult.HandFull;
            return false;
        }

        var success = board.TryTakePublicDevelopment(_developmentToReserve, out result);

        if (success && board.GetTokenCount(Token.Gold) > 0)
        {
            board.RemoveToken(Token.Gold);
            player.AddToken(Token.Gold);
            player.TryReserve(_developmentToReserve);
        }

        return success;
    }
}
