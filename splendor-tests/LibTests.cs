using NUnit.Framework;
using splendor_lib;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test, TestCaseSource("ConstructorParams")]
        public void Test1(int level, int prestige, Color discounts, int diamondPrice, int rubyPrice, int emeraldPrice, int onyxPrice, int sapphirePrice)
        {
            //arrange
            Development d1, d2 = null;
            //act
            d1 = new Development(level, prestige, discounts,diamondPrice,rubyPrice,emeraldPrice,onyxPrice,sapphirePrice);
            d2 = new Development(level, prestige, discounts,diamondPrice,rubyPrice,emeraldPrice,onyxPrice,sapphirePrice);
            //assert
            Assert.Assert.AreEqual(d1,d2);
        }

        static object[] ConstructorParams =
        {
            new object[] { 12, 3, Color.White, 1,2,1,3,4 },
            new object[] { 7, 4, Color.Red, 1,2,6,7,4 },
            new object[] { 2, 6, Color.Blue, 1,2,6,3,4 },
            new object[] { 1, 0, Color.Black, 1,2,2,3,4 },
            new object[] { 7, 1, Color.Black, 1,2,6,2,4 },
            new object[] { 8, 2, Color.Red, 1,2,6,3,2 },
            new object[] { 3, 2, Color.White, 1,2,6,3,4 },
            new object[] { 4, 3, Color.White, 1,2,6,4,4 },
            new object[] { 7, 2, Color.Green, 2,2,7,3,4 },
            new object[] { 1, 1, Color.Red, 1,2,6,3,4 }
        };
    }
}