using System;
using System.Collections.Generic;
using System.Linq;

namespace splendor_lib;

public static class Tokens
{
    public static List<TokenColor> AllTokens => Enum.GetValues(typeof(TokenColor)).OfType<TokenColor>().ToList();
}