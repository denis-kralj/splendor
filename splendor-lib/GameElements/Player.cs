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
    public IReadOnlyTokenCollection BuyDevelopment(Development development)
    {
        if (!CanPay(development.Cost)) throw new System.Exception("we should have not come here!");

        TokenCollection payed = new TokenCollection();

        foreach (Token type in Tokens.AllTokens)
        {
            if(development.Cost.GetCount(type) <= Discount(type))
                continue;

            var discountedPrice = development.Cost.GetCount(type) - Discount(type);

            uint goldDiff = (uint)(discountedPrice - GetTokenCount(type));

            RemoveToken(Token.Gold, goldDiff);
            payed.AddTokens(Token.Gold, goldDiff);
            RemoveToken(type, discountedPrice - goldDiff);
            payed.AddTokens(type, discountedPrice - goldDiff);
        }

        _purchasedDevelopmentsInternal.Add(development);

        return payed;
    } 
    public uint Discount(Token type) => (uint)_purchasedDevelopmentsInternal.Count(d => d.Discounts == type);

    public bool TryReserve(Development development)
    {
        if (HandFull) return false;

        ReservedDevelopments.Add(development);

        return true;
    }

    public bool CanPay(IReadOnlyTokenCollection price)
    {
        uint usableGold = GetTokenCount(Token.Gold);

        foreach (Token type in Tokens.AllTokens)
        {
            if(price.GetCount(type) <= Discount(type))
                continue;

            uint discountedPrice = price.GetCount(type) - Discount(type);

            uint have = GetTokenCount(type);

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

            uint goldDiff = (uint)(discountedPrice - GetTokenCount(type));

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

    public void AddToken(Token type, uint count = 1)
    {
        _tokensInternal.AddTokens(type, count);
    }

    public void RemoveToken(Token type, uint count = 1)
    {
        if (count > GetTokenCount(type))
        {
            throw new System.Exception("should type this exception");
        }

        _tokensInternal.TryTake(type, count);
    }

    public uint GetTokenCount(Token type)
    {
        return _tokensInternal.GetCount(type);
    }
}
