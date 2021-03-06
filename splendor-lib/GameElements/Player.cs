using System.Collections.Generic;
using System.Linq;

namespace splendor_lib
{
    public class Player
    {
        private TokenCollection _tokensInternal;
        private List<Development> _purchasedDevelopmentsInternal;
        private List<Noble> _noblesInternal;
        private List<Development> _reservedDevelopmentsInternal;
        public Player(string name) => SetInitState(name);
        public string PlayerName { get; private set; }
        public uint Prestige => (uint)(_purchasedDevelopmentsInternal.Sum(d => d.Prestige) + _noblesInternal.Sum(n => n.Prestige));
        public uint TotalOwnedTokens => _tokensInternal.TotalTokens;
        public bool HasTooManyTokens => TotalOwnedTokens > 10;
        public bool HandFull => _reservedDevelopmentsInternal.Count == 3;
        public List<Development> ReservedDevelopments => _reservedDevelopmentsInternal;
        public void TakeNoble(Noble noble) => _noblesInternal.Add(noble);
        public bool TryRemoveReserved(Development developmentToBuy) => _reservedDevelopmentsInternal.Remove(developmentToBuy);
        public void BuyDevelopment(Development development) => _purchasedDevelopmentsInternal.Add(development);
        public void ResetPlayer() => SetInitState(string.Empty);
        public void CollectTokens(TokenCollection tokens) => _tokensInternal.AddTokens(tokens);
        public void CollectTokens(TokenColor tokenColor, uint amountToAdd) => _tokensInternal.AddTokens(tokenColor, amountToAdd);
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

            foreach (TokenColor tokenColor in TokenUtils.AllTokens)
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

            foreach (TokenColor key in TokenUtils.AllTokens)
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
        private void SetInitState(string name)
        {
            if(!string.IsNullOrEmpty(name))
                PlayerName = name;

            _tokensInternal = new TokenCollection();
            _purchasedDevelopmentsInternal = new List<Development>();
            _noblesInternal = new List<Noble>();
            _reservedDevelopmentsInternal = new List<Development>(3);
        }
    }
}