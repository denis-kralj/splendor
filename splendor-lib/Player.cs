using System;
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
        public Player()
        {
            SetInitState();
        }
        private void SetInitState()
        {
            _tokensInternal = new TokenCollection();
            _purchasedDevelopmentsInternal = new List<Development>();
            _noblesInternal = new List<Noble>();
            _reservedDevelopmentsInternal = new List<Development>(3);
        }
        public int Prestige => _purchasedDevelopmentsInternal.Sum(d => d.Prestige) + _noblesInternal.Sum(n => n.Prestige);
        public bool HasTooManyTokens => _tokensInternal.TotalTokens > 10;
        public bool HandFull => _reservedDevelopmentsInternal.Count == 3;
        public void TakeNoble(Noble noble) => _noblesInternal.Add(noble);
        public void GetDevelopment(Development development) => _purchasedDevelopmentsInternal.Add(development);
        public void ResetPlayer() => SetInitState();
        public void CollectTokens(TokenCollection tokens) => _tokensInternal.AddTokens(tokens);
        public void CollectTokens(Token tokenType, uint amountToAdd) => _tokensInternal.AddTokens(tokenType, amountToAdd);
        public uint TokenCount(Token type) => _tokensInternal.GetCount(type);
        public int Discount(Token type) => _purchasedDevelopmentsInternal.Count(d => d.Discounts == type);
        public bool TryReserve(Development development)
        {
            if (HandFull) return false;

            _reservedDevelopmentsInternal.Add(development);

            return true;
        }
        public bool CanPay(TokenCollection price)
        {
            uint usableGold = _tokensInternal.GetCount(Token.Yellow);

            foreach (Token color in Enum.GetValues(typeof(Token)))
            {
                int discountedPrice = (int)price.GetCount(color) - _purchasedDevelopmentsInternal.Count(d => d.Discounts == color);

                if (discountedPrice < 1)
                    continue;

                uint have = _tokensInternal.GetCount(color);

                if (discountedPrice > usableGold + have)
                    return false;

                var needGold = discountedPrice - have < 0;

                if (needGold)
                    usableGold -= (uint)(discountedPrice - have);
            }

            return true;
        }

        public bool TryPay(TokenCollection price)
        {
            if (!CanPay(price)) return false;

            foreach (Token key in Enum.GetValues(typeof(Token)))
            {
                var discountedPrice = price.GetCount(key) - Discount(key);

                if (discountedPrice < 1)
                    continue;

                uint goldDiff = (uint)(discountedPrice - TokenCount(key));

                if (goldDiff > 0)
                {
                    _tokensInternal.TryTake(Token.Yellow, goldDiff);
                    price.TryTake(key, goldDiff);
                }
                else
                    return false;
            }

            _tokensInternal.TryTake(price);
            return true;
        }
    }
}