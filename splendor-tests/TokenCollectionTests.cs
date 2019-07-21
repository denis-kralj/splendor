using NUnit.Framework;
using splendor_lib;

namespace splendor_tests
{
    public class TokenCollectionTests
    {
        private TokenCollection _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new TokenCollection();
        }

        [Test]
        public void ShouldAddTokensToCollectionByFirstOverride()
        {
            var tokenCountToAdd = 3;
            var countBefore = _sut.TotalTokens;

            _sut.AddTokens(new TokenCollection(whiteCount: tokenCountToAdd));

            Assert.AreEqual(countBefore + tokenCountToAdd, _sut.TotalTokens);
        }

        [Test]
        public void ShouldAddTokensToCollectionBySecondOverride()
        {
            var tokenCountToAdd = 5;
            var countBefore = _sut.TotalTokens;

            _sut.AddTokens(Token.Green, tokenCountToAdd);

            Assert.AreEqual(countBefore + tokenCountToAdd, tokenCountToAdd);
        }

        [Test]
        public void ShouldRemoveTokens()
        {
            var blackAddCount = 2;
            var blueAddCount = 1;
            var blackRemoveCount = 1;
            var blueRemoveCount = 1;
            var totalBeforeAdd = _sut.TotalTokens;

            _sut.AddTokens(new TokenCollection(blackCount: blackAddCount, blueCount: blueAddCount));

            var totalAfterAdd = totalBeforeAdd + blackAddCount + blueAddCount;

            Assert.AreEqual(totalAfterAdd, _sut.TotalTokens);

            Assert.IsTrue(_sut.TryTake(new TokenCollection(blackCount: blackRemoveCount, blueCount: blueRemoveCount)));

            var totalAfterTake = totalAfterAdd - blackRemoveCount - blueRemoveCount;

            Assert.AreEqual(totalAfterTake, _sut.TotalTokens);
        }

        [Test]
        public void ShouldFailTryWhenRemovingTokensItDoesNotHave()
        {
            var redState = 0;
            _sut.SetCollectionState(redCount: redState);

            Assert.IsFalse(_sut.TryTake(Token.Red, redState + 1));
            Assert.IsFalse(_sut.TryTake(new TokenCollection(redCount: redState + 2)));
        }

        [Test]
        public void CanCountSpecificTokenTypeContained()
        {
            var blackCount = 2;
            var blueCount = 1;
            var greenCount = 0;
            var redCount = 2;
            var whiteCount = 0;
            var yellowCount = 1;

            _sut.AddTokens(new TokenCollection(whiteCount, blackCount, blueCount, greenCount, redCount, yellowCount));

            Assert.AreEqual(_sut.GetCount(Token.Black), blackCount);
            Assert.AreEqual(_sut.GetCount(Token.Blue), blueCount);
            Assert.AreEqual(_sut.GetCount(Token.Green), greenCount);
            Assert.AreEqual(_sut.GetCount(Token.Red), redCount);
            Assert.AreEqual(_sut.GetCount(Token.White), whiteCount);
            Assert.AreEqual(_sut.GetCount(Token.Yellow), yellowCount);
        }

        [Test]
        public void ShouldSetupCollection()
        {
            var whiteCount = 3;
            var blackCount = 5;
            var blueCount = 1;
            var greenCount = 0;
            var redCount = 11;
            var yellowCount = 6;

            _sut.SetCollectionState(whiteCount, blackCount, blueCount, greenCount, redCount, yellowCount);

            Assert.AreEqual(_sut.GetCount(Token.Black), blackCount);
            Assert.AreEqual(_sut.GetCount(Token.Blue), blueCount);
            Assert.AreEqual(_sut.GetCount(Token.Green), greenCount);
            Assert.AreEqual(_sut.GetCount(Token.Red), redCount);
            Assert.AreEqual(_sut.GetCount(Token.White), whiteCount);
            Assert.AreEqual(_sut.GetCount(Token.Yellow), yellowCount);
        }
    }
}