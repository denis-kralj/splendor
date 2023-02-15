using System.Collections.Generic;

namespace splendor_lib;

public interface IGameRunner
{
    public GameState GameState { get; }
    public IPlayer CurrentPlayer { get; }
    public IBoard GameBoard { get; }
    public bool IsEndgame { get; }

    public void StartGame(IPlayer[] orderedPlayers);
    public void PassTurn();
    public void CurrentPlayerTakeAction(IGameAction action);
    public Dictionary<string, int> GetScoreTable();

}
