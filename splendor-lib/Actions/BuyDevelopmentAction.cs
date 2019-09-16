namespace splendor_lib
{
    public class BuyDevelopmentAction : IGameAction
    {
        private Development _developmentToBuy;

        public BuyDevelopmentAction(Development developmentToBuy)
        {
            _developmentToBuy = developmentToBuy;
        }
        public bool TryExecuteAction(Player player, GameBoard board, out ExecutionResult result)
        {
            if(!(player.ReservedDevelopments.Contains(_developmentToBuy) || board.PublicDevelopments.Contains(_developmentToBuy)))
            {
                result = ExecutionResult.InvalidDevelopmentToBuy;
                return false;
            }

            if(!player.CanPay(_developmentToBuy.Cost))
            {
                result = ExecutionResult.InsufficientTokens;
                return false;
            }

            player.TryPay(_developmentToBuy.Cost);

            RemoveFromCurrentLocation(board, player);

            player.GetDevelopment(_developmentToBuy);

            result = ExecutionResult.Success;
            return true;
        }

        private void RemoveFromCurrentLocation(GameBoard board, Player player)
        {
                player.TryRemoveReserved(_developmentToBuy);
                board.TryRemoveDevelopment(Location.Public, _developmentToBuy, out var taken);
        }
    }
}