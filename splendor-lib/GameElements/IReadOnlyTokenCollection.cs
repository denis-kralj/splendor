namespace splendor_lib;

public interface IReadOnlyTokenCollection
{
    uint GetCount(TokenColor tokenColor);
    uint TotalTokens { get; }
}
