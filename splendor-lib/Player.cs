using System;
using System.Collections.Generic;
using System.Linq;

namespace splendor_lib
{
    public class Player
    {
        private TokenCollection _tokens;
        private List<Development> _developments;
        private List<Noble> _nobles;
        private List<Development> _hand;

        public Player()
        {
            SetInitState();
        }

        private void SetInitState()
        {
            _tokens = new TokenCollection();
            _developments = new List<Development>();
            _nobles = new List<Noble>();
            _hand = new List<Development>();
        }

        public int Prestige => _developments.Sum(d => d.Prestige) + _nobles.Sum(n => n.Prestige);

        public bool HasTooManyTokens => _tokens.TotalTokens > 10;

        public bool HandFull => _hand.Count == 3;

        public void Reserve(Development development)
        {
            if(HandFull)
                throw new PlayerHandFullException("Can't have more then 3 cards reserver");

            _hand.Add(development);
        }

        public void GetDevelopment(Development development)
        {
            _developments.Add(development);
        }

        public void ResetPlayer()
        {
            SetInitState();
        }

        public  void TakeTokens(params Token[] tokens)
        {
            _tokens.AddTokens(tokens);
        }

        public int TokenCount(Token type)
        {
            return _tokens[type];
        }

        public int Discount(Token type)
        {
            return _developments.Count(d => d.Discounts == type);
        }

        public bool CanPay(IReadOnlyDictionary<Token,int> price)
        {
            var usableGold = _tokens[Token.Yellow];

            foreach(Token color in Enum.GetValues(typeof(Token)))
            {
                if(!price.Keys.Contains(color)) continue;

                var have = _tokens[color];
                var discountedPrice = price[color] - _developments.Count(d => d.Discounts == color);

                while(discountedPrice > have && usableGold > 0)
                {
                    usableGold--;
                    have++;
                }

                if(discountedPrice > have) return false;
            }

            return true;
        }

        public void Pay(IReadOnlyDictionary<Token,int> price)
        {
            if(!CanPay(price))
                throw new InsufficientTokensException("Unable to pay price with current tokens");

            foreach(Token key in price.Keys)
            {
                var discountedPrice = price[key] - _developments.Count(d => d.Discounts == key);

                while(discountedPrice > 0)
                {
                    if(_tokens[key] > 0)
                        _tokens.RemoveTokens(key);
                    else
                        _tokens.RemoveTokens(Token.Yellow);

                    discountedPrice--;
                }
            }

        }
    }
}