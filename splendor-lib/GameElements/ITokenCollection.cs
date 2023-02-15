namespace splendor_lib;

public interface ITokenCollection
{
    uint TotalTokens { get; }
    bool AreAllSameType { get; }

    void AddTokens(TokenCollection tokensToAdd);
    void AddTokens(Token type, uint amountToAdd);
    bool TryTake(IReadOnlyTokenCollection tokensToTake);
    bool TryTake(Token type, uint count);
}
