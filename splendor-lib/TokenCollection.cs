using System;
using System.Collections.Generic;
using System.Linq;

namespace splendor_lib
{
    public class TokenCollection
    {
        private Dictionary<Token, uint> _tokensInternal;
        public TokenCollection(uint whiteCount = 0, uint blackCount = 0, uint blueCount = 0, uint greenCount = 0, uint redCount = 0, uint yellowCount = 0)
        {
            SetCollectionState(whiteCount, blackCount, blueCount, greenCount, redCount, yellowCount);
        }
        public uint GetCount(Token type) => _tokensInternal[type];
        public uint TotalTokens => (uint)_tokensInternal.Values.Sum(v => v);
        public void SetCollectionState(uint whiteCount = 0, uint blackCount = 0, uint blueCount = 0, uint greenCount = 0, uint redCount = 0, uint yellowCount = 0)
        {
            _tokensInternal = new Dictionary<Token, uint>
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
        public void AddTokens(Token tokenType, uint amountToAdd)
        {
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
        public bool TryTake(Token tokenType, uint count)
        {
            if (GetCount(tokenType) < count)
                return false;

            _tokensInternal[tokenType] -= count;

            return true;
        }
    }
}