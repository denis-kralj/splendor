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
            uint tokenCountToAdd = 3;
            var countBefore = _sut.TotalTokens;

            _sut.AddTokens(new TokenCollection(whiteCount: tokenCountToAdd));

            Assert.AreEqual(countBefore + tokenCountToAdd, _sut.TotalTokens);
        }

        [Test]
        public void ShouldAddTokensToCollectionBySecondOverride()
        {
            uint tokenCountToAdd = 5;
            var countBefore = _sut.TotalTokens;

            _sut.AddTokens(TokenColor.Green, tokenCountToAdd);

            Assert.AreEqual(countBefore + tokenCountToAdd, tokenCountToAdd);
        }

        [Test]
        public void ShouldRemoveTokens()
        {
            uint blackAddCount = 2;
            uint blueAddCount = 1;
            uint blackRemoveCount = 1;
            uint blueRemoveCount = 1;
            uint totalBeforeAdd = _sut.TotalTokens;

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
            uint redState = 0;
            _sut.SetCollectionState(redCount: redState);

            Assert.IsFalse(_sut.TryTake(TokenColor.Red, redState + 1));
            Assert.IsFalse(_sut.TryTake(new TokenCollection(redCount: redState + 2)));
        }

        [Test]
        public void CanCountSpecificTokenTypeContained()
        {
            uint blackCount = 2;
            uint blueCount = 1;
            uint greenCount = 0;
            uint redCount = 2;
            uint whiteCount = 0;
            uint yellowCount = 1;

            _sut.AddTokens(new TokenCollection(whiteCount, blackCount, blueCount, greenCount, redCount, yellowCount));

            Assert.AreEqual(_sut.GetCount(TokenColor.Black), blackCount);
            Assert.AreEqual(_sut.GetCount(TokenColor.Blue), blueCount);
            Assert.AreEqual(_sut.GetCount(TokenColor.Green), greenCount);
            Assert.AreEqual(_sut.GetCount(TokenColor.Red), redCount);
            Assert.AreEqual(_sut.GetCount(TokenColor.White), whiteCount);
            Assert.AreEqual(_sut.GetCount(TokenColor.Yellow), yellowCount);
        }

        [Test]
        public void ShouldSetupCollection()
        {
            uint whiteCount = 3;
            uint blackCount = 5;
            uint blueCount = 1;
            uint greenCount = 0;
            uint redCount = 11;
            uint yellowCount = 6;

            _sut.SetCollectionState(whiteCount, blackCount, blueCount, greenCount, redCount, yellowCount);

            Assert.AreEqual(_sut.GetCount(TokenColor.Black), blackCount);
            Assert.AreEqual(_sut.GetCount(TokenColor.Blue), blueCount);
            Assert.AreEqual(_sut.GetCount(TokenColor.Green), greenCount);
            Assert.AreEqual(_sut.GetCount(TokenColor.Red), redCount);
            Assert.AreEqual(_sut.GetCount(TokenColor.White), whiteCount);
            Assert.AreEqual(_sut.GetCount(TokenColor.Yellow), yellowCount);
        }
    }
}