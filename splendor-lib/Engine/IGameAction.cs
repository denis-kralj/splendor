namespace splendor_lib;

public interface IGameAction
{
    bool TryExecuteAction(IPlayer player, IBoard board, out ExecutionResult result);
}
