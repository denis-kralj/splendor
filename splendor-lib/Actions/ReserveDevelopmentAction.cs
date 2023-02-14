namespace splendor_lib;

public class ReserveDevelopmentAction : IGameAction
{
    private Location _location;
    private Development _developmentForReserving;
    private TokenCollection goldPayDay = new TokenCollection(yellowCount: 1);
    public ReserveDevelopmentAction(Location location, Development toReserve = null)
    {
        _location = location;
        _developmentForReserving = toReserve;
    }
    public bool TryExecuteAction(Player player, GameBoard board, out ExecutionResult result)
    {
        if (player.HandFull)
        {
            result = ExecutionResult.HandFull;
            return false;
        }

        if (board.TryRemoveDevelopment(_location, _developmentForReserving, out Development reserved))
        {
            if (board.TryTakeTokensFormBoard(goldPayDay))
                player.CollectTokens(goldPayDay);

            player.TryReserve(reserved);
            result = ExecutionResult.Success;
            return true;
        }

        result =
            _location == Location.Public ?
            ExecutionResult.InvalidDevelopmentToReserve :
            ExecutionResult.CantDraw;

        return false;
    }
}
