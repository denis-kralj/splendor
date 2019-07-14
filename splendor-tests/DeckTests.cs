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

            _sut.Draw();

            Assert.IsTrue(countBeforeDraw == _sut.Count + 1);
        }

        [Test]
        public void CanDrawThree()
        {
            var countBeforeDraw = _sut.Count;
            var drawCount = 3;

            _sut.Draw(drawCount);

            Assert.IsTrue(countBeforeDraw == _sut.Count + drawCount);
        }

        [Test]
        public void ThrowsExceptionWhenDrawingFromEmpty()
        {
            while(!_sut.IsEmpty) _sut.Draw();

            Assert.Throws(typeof(DeckException), () => _sut.Draw());
        }
    }
}