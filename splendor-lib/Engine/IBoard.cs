using System.Collections.Generic;

namespace splendor_lib;

public interface IBoard
{
    List<Noble> BoardNobles { get; }
    IReadOnlyTokenCollection BoardTokens { get; }
    List<Development> PublicDevelopments { get; }

    void AddTokensToBoard(TokenCollection tokensToReturnToBoard);
    void SetupBoard(PlayerCount playerCount, List<Noble> nobles, List<Development> developments);
    bool TryRemoveDevelopment(Location location, Development developmentToTake, out Development actuallyTaken);
    bool TryTakeTokensFormBoard(TokenCollection tokensToGetFromBoard);
}