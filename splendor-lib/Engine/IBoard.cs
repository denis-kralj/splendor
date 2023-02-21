using System.Collections.Generic;

namespace splendor_lib;

public interface IBoard : ITokenBank
{
    List<Noble> BoardNobles { get; }
    IReadOnlyTokenCollection BoardTokens { get; }
    List<Development> PublicDevelopments { get; }

    bool TryRemoveDevelopment(Location location, Development developmentToTake, out Development actuallyTaken);
    public bool TryTakeDeckDevelopment(DevelopmentDeck drawLocation, out Development development, out ExecutionResult result);
    bool TryTakePublicDevelopment(Development developmentToTake, out ExecutionResult executionResult);
}