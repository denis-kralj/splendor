using System;
using System.Collections.Generic;
using System.Linq;

namespace splendor_lib;

public enum Token
{
    Ruby,
    Sapphire,
    Emerald,
    Diamond,
    Onyx,
    Gold
}

public static class Tokens
{
    public static List<Token> AllTokens => Enum.GetValues(typeof(Token)).OfType<Token>().ToList();
}
