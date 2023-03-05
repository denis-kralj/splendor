using System.Collections.Generic;

namespace splendor_lib;

public interface IPlayer : ITokenBank
{
    string PlayerName { get; }
    uint Prestige { get; }
    bool HandFull { get; }
    List<Development> ReservedDevelopments { get; }

    IReadOnlyTokenCollection BuyDevelopment(Development development);
    bool CanPay(IReadOnlyTokenCollection price);
    uint Discount(Token type);
    void TakeNoble(Noble noble);
    bool TryPay(IReadOnlyTokenCollection price);
    bool TryRemoveReserved(Development developmentToBuy);
    bool TryReserve(Development development);
}
