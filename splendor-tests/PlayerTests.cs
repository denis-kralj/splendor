using System.Collections.Generic;
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
            _sut.TakeTokens(Token.Yellow, Token.Yellow, Token.Black);

            Assert.IsTrue(_sut.TokenCount(Token.Yellow) == 2);
            Assert.IsTrue(_sut.TokenCount(Token.Black) == 1);
        }

        [Test]
        public void ReservedDevelopmentsGenerateDiscounts()
        {
            _sut.GetDevelopment(new Development(1, 0, Token.Black, 0, 0, 0, 0, 0));
            _sut.GetDevelopment(new Development(2, 0, Token.Black, 0, 0, 0, 0, 0));

            Assert.AreEqual(2, _sut.Discount(Token.Black));
        }

        [Test]
        public void ThrowsExceptionWhenHandFull()
        {
            _sut.Reserve(new Development(1, 0, Token.Black, 0, 0, 0, 0, 0));
            _sut.Reserve(new Development(2, 0, Token.Black, 0, 0, 0, 0, 0));
            _sut.Reserve(new Development(3, 0, Token.Black, 0, 0, 0, 0, 0));

            Assert.Throws<PlayerHandFullException>(() => _sut.Reserve(new Development(4, 0, Token.Black, 0, 0, 0, 0, 0)));
        }

        [Test]
        public void HasCorrectPrestigeScore()
        {
            _sut.GetDevelopment(new Development(1, 1, Token.Black, 0, 0, 0, 0, 0));
            _sut.GetDevelopment(new Development(2, 2, Token.Black, 0, 0, 0, 0, 0));
            _sut.GetDevelopment(new Development(3, 3, Token.Black, 0, 0, 0, 0, 0));

            Assert.AreEqual(6, _sut.Prestige);
        }

        [Test]
        public void CanConfirmHeCanBuyIfHasFundsWithoutGold()
        {
            _sut.TakeTokens(Token.Black, Token.Black, Token.Black, Token.Blue, Token.Blue);

            var cost = new Dictionary<Token, int>{{Token.Black, 3}, {Token.Blue, 2}};

            Assert.IsTrue(_sut.CanPay(cost));
        }

        [Test]
        public void CanConfirmHeCanBuyIfHasFundsWithGold()
        {
            _sut.TakeTokens(Token.Black, Token.Black, Token.Yellow, Token.Blue, Token.Yellow);

            var cost = new Dictionary<Token, int>{{Token.Black, 3}, {Token.Blue, 2}};

            Assert.IsTrue(_sut.CanPay(cost));
        }

        [Test]
        public void CalculatesPrestigeWithTakenNobles()
        {
            var noble = new Noble(2,1,1,1,1,1);
            _sut.TakeNoble(noble);

            Assert.AreEqual(_sut.Prestige, noble.Prestige);
        }
    }
}