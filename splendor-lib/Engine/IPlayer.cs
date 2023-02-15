using System.Collections.Generic;

namespace splendor_lib;

public interface IPlayer
{
    string PlayerName { get; }
    uint Prestige { get; }
    bool HandFull { get; }
    List<Development> ReservedDevelopments { get; }

    void BuyDevelopment(Development development);
    bool CanPay(IReadOnlyTokenCollection price);
    void CollectTokens(TokenCollection tokens);
    uint Discount(Token type);
    void TakeNoble(Noble noble);
    uint TokenCount(Token type);
    bool TryPay(IReadOnlyTokenCollection price);
    bool TryRemoveReserved(Development developmentToBuy);
    bool TryReserve(Development development);
}
