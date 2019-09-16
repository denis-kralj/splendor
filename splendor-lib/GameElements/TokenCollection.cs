using System;
using System.Collections.Generic;
using System.Linq;

namespace splendor_lib
{
    public class TokenCollection : IReadOnlyTokenCollection
    {
        private Dictionary<TokenColor, uint> _tokensInternal;
        public TokenCollection(uint whiteCount = 0, uint blackCount = 0, uint blueCount = 0, uint greenCount = 0, uint redCount = 0, uint yellowCount = 0) => 
            SetCollectionState(whiteCount, blackCount, blueCount, greenCount, redCount, yellowCount);
        public uint GetCount(TokenColor tokenColor) => _tokensInternal[tokenColor];
        public uint TotalTokens => (uint)_tokensInternal.Values.Sum(v => v);
        public static bool operator ==(TokenCollection obj1, TokenCollection obj2) => obj1 as object != null && obj1.Equals(obj2);
        public static bool operator !=(TokenCollection obj1, TokenCollection obj2) => !(obj1 == obj2);
        public void AddTokens(TokenCollection tokensToAdd)
        {
            foreach (TokenColor tokenColor in TokenUtils.AllTokens)
                AddTokens(tokenColor, tokensToAdd.GetCount(tokenColor));
        }
        public void AddTokens(TokenColor tokenColor, uint amountToAdd) => _tokensInternal[tokenColor] += amountToAdd;
        public bool TryTake(IReadOnlyTokenCollection tokensToTake)
        {
            foreach (TokenColor key in TokenUtils.AllTokens)
                if (GetCount(key) < tokensToTake.GetCount(key))
                    return false;

            foreach (TokenColor key in TokenUtils.AllTokens)
                _tokensInternal[key] -= tokensToTake.GetCount(key);

            return true;
        }
        public bool TryTake(TokenColor tokenType, uint count)
        {
            if (GetCount(tokenType) < count)
                return false;

            _tokensInternal[tokenType] -= count;

            return true;
        }
        public void SetCollectionState(uint whiteCount = 0, uint blackCount = 0, uint blueCount = 0, uint greenCount = 0, uint redCount = 0, uint yellowCount = 0)
        {
            _tokensInternal = new Dictionary<TokenColor, uint>
            {
                { TokenColor.White,  whiteCount  },
                { TokenColor.Black,  blackCount  },
                { TokenColor.Blue,   blueCount   },
                { TokenColor.Green,  greenCount  },
                { TokenColor.Red,    redCount    },
                { TokenColor.Yellow, yellowCount }
            };
        }
        public override int GetHashCode() => base.GetHashCode();
        public override bool Equals(object obj) =>
            (obj != null) && (obj as TokenCollection != null) &&
            (obj as TokenCollection).TotalTokens == this.TotalTokens &&
            TokenUtils.AllTokens
            .All(t => (obj as TokenCollection).GetCount(t) == this.GetCount(t));
    }
}