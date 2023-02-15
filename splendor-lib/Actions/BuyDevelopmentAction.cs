namespace splendor_lib;

public class BuyDevelopmentAction : IGameAction
{
    private Development _developmentToBuy;

    public BuyDevelopmentAction(Development developmentToBuy)
    {
        _developmentToBuy = developmentToBuy;
    }

    public bool TryExecuteAction(IPlayer player, IBoard board, out ExecutionResult result)
    {
        if (DevelopmentIsNotInValidLocation(player, board))
        {
            result = ExecutionResult.InvalidDevelopmentToBuy;
            return false;
        }

        if (!player.CanPay(_developmentToBuy.Cost))
        {
            result = ExecutionResult.InsufficientTokens;
            return false;
        }

        player.TryPay(_developmentToBuy.Cost);
        RemoveFromCurrentLocation(board, player);
        player.BuyDevelopment(_developmentToBuy);

        result = ExecutionResult.Success;
        return true;
    }

    private bool DevelopmentIsNotInValidLocation(IPlayer player, IBoard board)
    {
        return !(
            player.ReservedDevelopments.Contains(_developmentToBuy) ||
            board.PublicDevelopments.Contains(_developmentToBuy)
        );
    }

    private void RemoveFromCurrentLocation(IBoard board, IPlayer player)
    {
        player.TryRemoveReserved(_developmentToBuy);
        board.TryRemoveDevelopment(Location.Public, _developmentToBuy, out _);
    }
}
