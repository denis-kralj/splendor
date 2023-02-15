using System.Collections.Generic;
using System.Linq;

namespace splendor_lib;

public class Player : IPlayer
{
    private TokenCollection _tokensInternal = new TokenCollection();
    private List<Development> _purchasedDevelopmentsInternal = new List<Development>();
    private List<Noble> _noblesInternal = new List<Noble>();
    private List<Development> _reservedDevelopmentsInternal = new List<Development>(3);
    public Player(string name)
    {
        PlayerName = name;
    }
    public string PlayerName { get; }
    public uint Prestige => (uint)(_purchasedDevelopmentsInternal.Sum(d => d.Prestige) + _noblesInternal.Sum(n => n.Prestige));
    public bool HandFull => _reservedDevelopmentsInternal.Count == 3;
    public List<Development> ReservedDevelopments => _reservedDevelopmentsInternal;
    public void TakeNoble(Noble noble) => _noblesInternal.Add(noble);
    public bool TryRemoveReserved(Development developmentToBuy) => _reservedDevelopmentsInternal.Remove(developmentToBuy);
    public void BuyDevelopment(Development development) => _purchasedDevelopmentsInternal.Add(development);
    public void CollectTokens(TokenCollection tokens) => _tokensInternal.AddTokens(tokens);
    public uint TokenCount(TokenColor tokenColor) => _tokensInternal.GetCount(tokenColor);
    public uint Discount(TokenColor tokenColor) => (uint)_purchasedDevelopmentsInternal.Count(d => d.Discounts == tokenColor);
    public bool TryReserve(Development development)
    {
        if (HandFull) return false;

        _reservedDevelopmentsInternal.Add(development);

        return true;
    }
    public bool CanPay(IReadOnlyTokenCollection price)
    {
        uint usableGold = TokenCount(TokenColor.Yellow);

        foreach (TokenColor tokenColor in Tokens.AllTokens)
        {
            uint discountedPrice = price.GetCount(tokenColor) - Discount(tokenColor);

            if (discountedPrice < 1)
                continue;

            uint have = TokenCount(tokenColor);

            if (discountedPrice > usableGold + have)
                return false;

            var needGold = discountedPrice - have < 0;

            if (needGold)
                usableGold -= (discountedPrice - have);
        }

        return true;
    }
    public bool TryPay(IReadOnlyTokenCollection price)
    {
        if (!CanPay(price)) return false;

        foreach (TokenColor key in Tokens.AllTokens)
        {
            var discountedPrice = price.GetCount(key) - Discount(key);

            if (discountedPrice < 1)
                continue;

            uint goldDiff = (uint)(discountedPrice - TokenCount(key));

            if (goldDiff > 0)
            {
                _tokensInternal.TryTake(TokenColor.Yellow, goldDiff);
                _tokensInternal.AddTokens(key, goldDiff);
            }
            else
                return false;
        }

        _tokensInternal.TryTake(price);
        return true;
    }
}
