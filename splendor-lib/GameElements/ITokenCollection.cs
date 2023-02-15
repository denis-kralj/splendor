namespace splendor_lib;

public interface ITokenCollection
{
    uint TotalTokens { get; }
    bool AreAllSameType { get; }

    void AddTokens(TokenCollection tokensToAdd);
    void AddTokens(TokenColor tokenColor, uint amountToAdd);
    bool TryTake(IReadOnlyTokenCollection tokensToTake);
    bool TryTake(TokenColor tokenType, uint count);
}
