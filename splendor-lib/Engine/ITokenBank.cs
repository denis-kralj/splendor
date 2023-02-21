namespace splendor_lib;

public interface ITokenBank
{
    void AddToken(Token type, uint count = 1);
    void RemoveToken(Token type, uint count = 1);
    uint GetTokenCount(Token type);
}