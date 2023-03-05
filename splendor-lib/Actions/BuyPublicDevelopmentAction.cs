namespace splendor_lib;

public class BuyPublicDevelopmentAction : IGameAction
{
    private readonly Development _developmentToBuy;

    public BuyPublicDevelopmentAction(Development developmentToBuy)
    {
        _developmentToBuy = developmentToBuy;
    }
    public bool TryExecuteAction(IPlayer player, IBoard board, out ExecutionResult result)
    {
        if (board.DoesNotContainDevelopment(_developmentToBuy))
        {
            result = ExecutionResult.InvalidDevelopmentToBuy;
            return false;
        }

        if (player.CanNotPayFor(_developmentToBuy))
        {
            result = ExecutionResult.InsufficientTokens;
            return false;
        }

        board.TakePublic(_developmentToBuy);
        var payed = player.BuyDevelopment(_developmentToBuy);
        board.Gets(payed);

        result = ExecutionResult.Success;
        return true;
    }
}
