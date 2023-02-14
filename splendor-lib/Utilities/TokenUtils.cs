using System;
using System.Collections.Generic;
using System.Linq;

namespace splendor_lib;

public static class TokenUtils
{
    public static List<TokenColor> AllTokens => Enum.GetValues(typeof(TokenColor)).OfType<TokenColor>().ToList();
    public static TokenCollection ToCollection(string input)
    {
        var output = new TokenCollection();

        foreach (char c in input)
        {
            var element = char.ToLower(c);
            if (!_register.Keys.Contains(element))
            {
                var allowedElements = string.Join(",", _register.Keys);
                var msg = $"Input sequence contains invalid element, accepted elements are [{allowedElements}]";
                throw new ArgumentException(msg, nameof(input));
            }

            output.AddTokens(_register[element], 1);
        }

        return output;
    }
    private static Dictionary<char, TokenColor> _register
    =>
        new Dictionary<char, TokenColor>
        {
            { 'o', TokenColor.Black  }, // Onix
            { 's', TokenColor.Blue   }, // Sapphire
            { 'e', TokenColor.Green  }, // Emerald
            { 'r', TokenColor.Red    }, // Ruby
            { 'd', TokenColor.White  }, // Diamond
            { 'g', TokenColor.Yellow }  // Gold
        };
}
