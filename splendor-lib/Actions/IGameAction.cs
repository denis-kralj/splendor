namespace splendor_lib
{
    public interface IGameAction
    {
        bool TryExecuteAction(Player player, GameBoard board, out ExecutionResult result);
    }
}