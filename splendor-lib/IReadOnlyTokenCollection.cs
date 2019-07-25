namespace splendor_lib
{
    public interface IReadOnlyTokenCollection
    {
         
        uint GetCount(Token type);
        uint TotalTokens { get; }
    }
}