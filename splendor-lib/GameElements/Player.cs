using System.Collections.Generic;
using System.Linq;

namespace splendor_lib;

public class Player : IPlayer
{
    private TokenCollection _tokensInternal = new TokenCollection();
    private List<Development> _purchasedDevelopmentsInternal = new List<Development>();
    private List<Noble> _noblesInternal = new List<Noble>();

    public Player(string name)
    {
        PlayerName = name;
        ReservedDevelopments = new List<Development>();

    }

    public string PlayerName { get; }
    public List<Development> ReservedDevelopments { get; }

    public uint Prestige => (uint)(_purchasedDevelopmentsInternal.Sum(d => d.Prestige) + _noblesInternal.Sum(n => n.Prestige));
    public bool HandFull => ReservedDevelopments.Count == 3;

    public void TakeNoble(Noble noble) => _noblesInternal.Add(noble);
    public bool TryRemoveReserved(Development developmentToBuy) => ReservedDevelopments.Remove(developmentToBuy);
    public void BuyDevelopment(Development development) => _purchasedDevelopmentsInternal.Add(development);
    public void CollectTokens(TokenCollection tokens) => _tokensInternal.AddTokens(tokens);
    public uint TokenCount(Token type) => _tokensInternal.GetCount(type);
    public uint Discount(Token type) => (uint)_purchasedDevelopmentsInternal.Count(d => d.Discounts == type);

    public bool TryReserve(Development development)
    {
        if (HandFull) return false;

        ReservedDevelopments.Add(development);

        return true;
    }

    public bool CanPay(IReadOnlyTokenCollection price)
    {
        uint usableGold = TokenCount(Token.Gold);

        foreach (Token type in Tokens.AllTokens)
        {
            uint discountedPrice = price.GetCount(type) - Discount(type);

            if (discountedPrice < 1)
                continue;

            uint have = TokenCount(type);

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

        foreach (Token type in Tokens.AllTokens)
        {
            var discountedPrice = price.GetCount(type) - Discount(type);

            if (discountedPrice < 1)
                continue;

            uint goldDiff = (uint)(discountedPrice - TokenCount(type));

            if (goldDiff > 0)
            {
                _tokensInternal.TryTake(Token.Gold, goldDiff);
                _tokensInternal.AddTokens(type, goldDiff);
            }
            else
                return false;
        }

        _tokensInternal.TryTake(price);
        return true;
    }
}
