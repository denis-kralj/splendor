using System;
using System.Collections.Generic;
using System.Linq;

namespace splendor_lib
{
    public class TokenCollection : IReadOnlyTokenCollection
    {
        private Dictionary<Token, uint> _tokensInternal;
        public TokenCollection(uint whiteCount = 0, uint blackCount = 0, uint blueCount = 0, uint greenCount = 0, uint redCount = 0, uint yellowCount = 0) => 
            SetCollectionState(whiteCount, blackCount, blueCount, greenCount, redCount, yellowCount);
        public uint GetCount(Token type) => _tokensInternal[type];
        public uint TotalTokens => (uint)_tokensInternal.Values.Sum(v => v);
        public static bool operator ==(TokenCollection obj1, TokenCollection obj2) => obj1 as object != null && obj1.Equals(obj2);
        public static bool operator !=(TokenCollection obj1, TokenCollection obj2) => !(obj1 == obj2);
        public void AddTokens(TokenCollection tokensToAdd)
        {
            foreach (Token key in Enum.GetValues(typeof(Token)))
                AddTokens(key, tokensToAdd.GetCount(key));
        }
        public void AddTokens(Token tokenType, uint amountToAdd) => _tokensInternal[tokenType] += amountToAdd;
        public bool TryTake(TokenCollection tokensToTake)
        {
            foreach (Token key in Enum.GetValues(typeof(Token)))
                if (GetCount(key) < tokensToTake.GetCount(key))
                    return false;

            foreach (Token key in Enum.GetValues(typeof(Token)))
                _tokensInternal[key] -= tokensToTake.GetCount(key);

            return true;
        }
        public bool TryTake(Token tokenType, uint count)
        {
            if (GetCount(tokenType) < count)
                return false;

            _tokensInternal[tokenType] -= count;

            return true;
        }
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
        public override int GetHashCode() => base.GetHashCode();
        public override bool Equals(object obj) =>
            (obj != null) && (obj as TokenCollection != null) &&
            (obj as TokenCollection).TotalTokens == this.TotalTokens &&
            Enum.GetValues(typeof(Token)).Cast<Token>()
            .All(t => (obj as TokenCollection).GetCount(t) == this.GetCount(t));
    }
}