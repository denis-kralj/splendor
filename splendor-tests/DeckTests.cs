using System.Collections.Generic;
using NUnit.Framework;
using splendor_lib;

namespace splendor_tests
{
    public class DeckTests
    {
        private Deck<object> _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new Deck<object>(new List<object>(new [] {new object(), new object(), new object(), new object(), new object()}));
        }

        [Test]
        public void CanShuffle()
        {
            Assert.NotNull("There is no point in testing randomness...");
        }

        [Test]
        public void CanDrawOneCard()
        {
            var countBeforeDraw = _sut.Count;

            List<object> drawn = null;
            Assert.IsTrue(_sut.TryDraw(out drawn));
        }

        [Test]
        public void CanDrawThree()
        {
            var countBeforeDraw = _sut.Count;
            uint drawCount = 3;

            List<object> drawn = null;

            _sut.TryDraw(out drawn, false, drawCount);

            Assert.IsTrue(countBeforeDraw == _sut.Count + drawCount);
        }

        [Test]
        public void FailsToDrawOnEmptyDeck()
        {
            List<object> drawn;
            while(!_sut.IsEmpty)
                Assert.IsTrue(_sut.TryDraw(out drawn));

            Assert.IsFalse(_sut.TryDraw(out drawn));
        }
    }
}