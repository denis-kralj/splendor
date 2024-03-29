namespace splendor_lib;

public interface IReadOnlyTokenCollection
{
    uint TotalTokens { get; }

    uint GetCount(Token type);
}
