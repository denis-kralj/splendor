using System.Collections.Generic;

namespace splendor_lib;

public interface IBoard
{
    List<Noble> BoardNobles { get; }
    IReadOnlyTokenCollection BoardTokens { get; }
    List<Development> PublicDevelopments { get; }

    void AddTokensToBoard(TokenCollection tokensToReturnToBoard);
    bool TryRemoveDevelopment(Location location, Development developmentToTake, out Development actuallyTaken);
    bool TryTakeTokensFormBoard(TokenCollection tokensToGetFromBoard);
}