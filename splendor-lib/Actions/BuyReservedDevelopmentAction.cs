namespace splendor_lib;

public class BuyReservedDevelopmentAction : IGameAction
{
    private readonly Development _developmentToBuy;

    public BuyReservedDevelopmentAction(Development developmentToBuy)
    {
        _developmentToBuy = developmentToBuy;
    }
    public bool TryExecuteAction(IPlayer player, IBoard board, out ExecutionResult result)
    {
        if (player.DidNotReserve(_developmentToBuy))
        {
            result = ExecutionResult.InvalidDevelopmentToBuy;
            return false;
        }

        if (player.CanNotPayFor(_developmentToBuy))
        {
            result = ExecutionResult.InsufficientTokens;
            return false;
        }

        player.RemoveReserver(_developmentToBuy);
        var payed = player.BuyDevelopment(_developmentToBuy);
        board.Gets(payed);

        result = ExecutionResult.Success;
        return true;
    }
}