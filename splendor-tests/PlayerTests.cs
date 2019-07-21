using System.Linq;
using NUnit.Framework;
using splendor_lib;

namespace splendor_tests
{
    public class PlayerTests
    {
        private Player _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new Player();
        }

        [Test]
        public void PlayerIsInitializedWithoutTokens()
        {
            Assert.IsTrue(_sut.TokenCount(Token.Black) == 0);
            Assert.IsTrue(_sut.TokenCount(Token.Blue) == 0);
            Assert.IsTrue(_sut.TokenCount(Token.Green) == 0);
            Assert.IsTrue(_sut.TokenCount(Token.Red) == 0);
            Assert.IsTrue(_sut.TokenCount(Token.White) == 0);
            Assert.IsTrue(_sut.TokenCount(Token.Yellow) == 0);
        }

        [Test]
        public void CanGetMoreTokens()
        {
            uint yellowCount = 2;
            uint blackCount = 1;
            _sut.CollectTokens(new TokenCollection(yellowCount: yellowCount, blackCount: blackCount));

            Assert.IsTrue(_sut.TokenCount(Token.Yellow) == yellowCount);
            Assert.IsTrue(_sut.TokenCount(Token.Black) == blackCount);
        }

        [Test]
        public void AquiredDevelopmentsGenerateDiscounts()
        {
            var numberOfBlackDiscounts = 4;
            for (uint i = 0; i < numberOfBlackDiscounts; i++)
                _sut.GetDevelopment(new Development(i, 0, Token.Black, new TokenCollection()));

            Assert.AreEqual(numberOfBlackDiscounts, _sut.Discount(Token.Black));
        }

        [Test]
        public void FailsToReserveWhenHandFull()
        {
            var price = new TokenCollection();
            Assert.IsTrue(_sut.TryReserve(new Development(1, 0, Token.Black, price)));
            Assert.IsTrue(_sut.TryReserve(new Development(2, 0, Token.Black, price)));
            Assert.IsTrue(_sut.TryReserve(new Development(3, 0, Token.Black, price)));
            Assert.IsFalse(_sut.TryReserve(new Development(4, 0, Token.Black, price)));
        }

        [Test]
        public void HasCorrectPrestigeScore()
        {
            var scored = new uint[] { 1, 2, 3 };

            foreach (var score in scored)
                _sut.GetDevelopment(new Development(1, score, Token.Black, new TokenCollection()));

            Assert.AreEqual(scored.ToList().Sum(e => e), _sut.Prestige);
        }

        [Test]
        public void CanConfirmHeCanBuyIfHasFundsWithoutGold()
        {
            uint blackCount = 3;
            uint blueCount = 2;

            _sut.CollectTokens(new TokenCollection(blackCount: blackCount, blueCount: blueCount));

            uint blackCost = 2;
            uint blueCost = 2;

            var cost = new TokenCollection(blackCount: blackCost, blueCount: blueCost);

            Assert.IsTrue(_sut.CanPay(cost));
        }

        [Test]
        public void CanConfirmHeCanBuyIfHasFundsWithGold()
        {
            uint blackCount = 1;
            uint blueCount = 2;
            uint yellowCount = 3;

            _sut.CollectTokens(new TokenCollection(blackCount: blackCount, blueCount: blueCount, yellowCount: yellowCount));

            uint blackCost = 3;
            uint blueCost = 3;
            var cost = new TokenCollection(blackCount: blackCost, blueCount: blueCost);

            Assert.IsTrue(_sut.CanPay(cost));
        }

        [Test]
        public void CalculatesPrestigeWithTakenNobles()
        {
            uint noblePrestige = 2;
            var noble = new Noble(noblePrestige, 1, 1, 1, 1, 1);
            _sut.TakeNoble(noble);

            Assert.AreEqual(_sut.Prestige, noblePrestige);
        }
    }
}