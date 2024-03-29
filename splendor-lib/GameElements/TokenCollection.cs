using System.Collections.Generic;
using System.Linq;

namespace splendor_lib;

public class TokenCollection : IReadOnlyTokenCollection, ITokenCollection
{
    private Dictionary<Token, uint> _tokensInternal;

    public TokenCollection(
        uint  diamondCount = 0,
        uint     onyxCount = 0,
        uint sapphireCount = 0,
        uint  emeraldCount = 0,
        uint     rubyCount = 0,
        uint     goldCount = 0
    )
    {
        _tokensInternal = new Dictionary<Token, uint>
            {
                { Token.Diamond,  diamondCount  },
                { Token.Onyx,     onyxCount  },
                { Token.Sapphire, sapphireCount   },
                { Token.Emerald,  emeraldCount  },
                { Token.Ruby,     rubyCount    },
                { Token.Gold,     goldCount }
            };
    }

    public uint GetCount(Token type) => _tokensInternal[type];
    public uint TotalTokens => (uint)_tokensInternal.Values.Sum(v => v);
    public bool AreAllSameType => _tokensInternal.Values.Count(e => e == 0) >= 5;
    public void AddTokens(Token type, uint amountToAdd) => _tokensInternal[type] += amountToAdd;

    public void AddTokens(TokenCollection tokensToAdd)
    {
        foreach (Token type in Tokens.AllTokens)
            AddTokens(type, tokensToAdd.GetCount(type));
    }

    public bool TryTake(IReadOnlyTokenCollection tokensToTake)
    {
        foreach (Token type in Tokens.AllTokens)
            if (GetCount(type) < tokensToTake.GetCount(type))
                return false;

        foreach (Token type in Tokens.AllTokens)
            _tokensInternal[type] -= tokensToTake.GetCount(type);

        return true;
    }

    public bool TryTake(Token type, uint count)
    {
        if (GetCount(type) < count)
            return false;

        _tokensInternal[type] -= count;

        return true;
    }

    public static bool operator ==(TokenCollection obj1, TokenCollection obj2) => obj1 as object != null && obj1.Equals(obj2);

    public static bool operator !=(TokenCollection obj1, TokenCollection obj2) => !(obj1 == obj2);

    public override int GetHashCode() => base.GetHashCode();

    public override bool Equals(object obj) =>
        (obj != null) && (obj as TokenCollection != null) &&
        (obj as TokenCollection).TotalTokens == this.TotalTokens &&
        Tokens.AllTokens
        .All(t => (obj as TokenCollection).GetCount(t) == this.GetCount(t));
}
