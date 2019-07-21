using System;
using System.Collections.Generic;
using System.Linq;

namespace splendor_lib
{
    public class TokenCollection
    {
        private Dictionary<Token, int> _tokensInternal;
        public TokenCollection(int whiteCount = 0, int blackCount = 0, int blueCount = 0, int greenCount = 0, int redCount = 0, int yellowCount = 0)
        {
            SetCollectionState(whiteCount, blackCount, blueCount, greenCount, redCount, yellowCount);
        }
        public int GetCount(Token type) => _tokensInternal[type];
        public int TotalTokens => _tokensInternal.Values.Sum(v => v);
        public void SetCollectionState(int whiteCount = 0, int blackCount = 0, int blueCount = 0, int greenCount = 0, int redCount = 0, int yellowCount = 0)
        {
            if(whiteCount < 0 || blackCount < 0 || blueCount < 0 || greenCount < 0 || redCount < 0 || yellowCount < 0)
                throw new ArgumentOutOfRangeException("Token count cannot be set to a negative value");

            _tokensInternal = new Dictionary<Token, int>
            {
                { Token.White,  whiteCount  },
                { Token.Black,  blackCount  },
                { Token.Blue,   blueCount   },
                { Token.Green,  greenCount  },
                { Token.Red,    redCount    },
                { Token.Yellow, yellowCount }
            };
        }
        public void AddTokens(TokenCollection tokensToAdd)
        {
            foreach (Token key in Enum.GetValues(typeof(Token)))
                _tokensInternal[key] += tokensToAdd.GetCount(key);
        }
        public void AddTokens(Token tokenType, int amountToAdd)
        {
            if(amountToAdd < 0)
                throw new ArgumentOutOfRangeException(nameof(amountToAdd), amountToAdd, "Token count cannot be negative");

            _tokensInternal[tokenType] += amountToAdd;
        }

        public bool TryTake(TokenCollection tokens)
        {
            foreach (Token key in Enum.GetValues(typeof(Token)))
                if (GetCount(key) < tokens.GetCount(key))
                    return false;

            foreach (Token key in Enum.GetValues(typeof(Token)))
                _tokensInternal[key] -= tokens.GetCount(key);

            return true;
        }
        public bool TryTake(Token tokenType, int count)
        {
            if (count < 0 || GetCount(tokenType) < count)
                return false;

            _tokensInternal[tokenType] -= count;

            return true;
        }
    }
}