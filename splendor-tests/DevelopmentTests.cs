using NUnit.Framework;
using splendor_lib;

namespace splendor_tests;

public class DevelopmentTests
{
    [Test, TestCaseSource(nameof(ConstructorParams))]
    public void SameValueDevelopmentsAreEqual(uint level, uint prestige, TokenColor discounts, uint diamondPrice, uint rubyPrice, uint emeraldPrice, uint onyxPrice, uint sapphirePrice)
    {
        Development d1, d2 = null;

        var price = new TokenCollection(diamondPrice, onyxPrice, sapphirePrice, emeraldPrice, rubyPrice, 0);
        d1 = new Development(level, prestige, discounts, price);
        d2 = new Development(level, prestige, discounts, price);

        Assert.AreEqual(d1, d2);
    }

    [Test, TestCaseSource(nameof(ConstructorParams))]
    public void SameValueDevelopmentsAreNotEqual(uint level, uint prestige, TokenColor discounts, uint diamondPrice, uint rubyPrice, uint emeraldPrice, uint onyxPrice, uint sapphirePrice)
    {
        Development d1, d2 = null;

        var price = new TokenCollection(diamondPrice, onyxPrice, sapphirePrice, emeraldPrice, rubyPrice, 0);
        var differentPrice = new TokenCollection(diamondPrice + 1, onyxPrice + 2, sapphirePrice + 4, emeraldPrice + 1, rubyPrice + 1, 0);
        d1 = new Development(level, prestige, discounts, price);
        d2 = new Development(level - 1, prestige + 1, discounts, differentPrice);

        Assert.AreNotEqual(d1, d2);
    }

    static object[] ConstructorParams =
    {
        new object[] { (uint)12, (uint)3, TokenColor.White, (uint)1,(uint)2,(uint)1,(uint)3,(uint)4 },
        new object[] { (uint)7, (uint)4, TokenColor.Red, (uint)1,(uint)2,(uint)6,(uint)7,(uint)4 },
        new object[] { (uint)2, (uint)6, TokenColor.Blue, (uint)1,(uint)2,(uint)6,(uint)3,(uint)4 },
        new object[] { (uint)1, (uint)0, TokenColor.Black, (uint)1,(uint)2,(uint)2,(uint)3,(uint)4 },
        new object[] { (uint)7, (uint)1, TokenColor.Black, (uint)1,(uint)2,(uint)6,(uint)2,(uint)4 },
        new object[] { (uint)8, (uint)2, TokenColor.Red, (uint)1,(uint)2,(uint)6,(uint)3,(uint)2 },
        new object[] { (uint)3, (uint)2, TokenColor.White, (uint)1,(uint)2,(uint)6,(uint)3,(uint)4 },
        new object[] { (uint)4, (uint)3, TokenColor.White, (uint)1,(uint)2,(uint)6,(uint)4,(uint)4 },
        new object[] { (uint)7, (uint)2, TokenColor.Green, (uint)2,(uint)2,(uint)7,(uint)3,(uint)4 },
        new object[] { (uint)1, (uint)1, TokenColor.Red, (uint)1,(uint)2,(uint)6,(uint)3,(uint)4 }
    };
}
