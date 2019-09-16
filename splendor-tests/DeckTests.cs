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
        public void CanDrawOneCard()
        {
            var countBeforeDraw = _sut.Count;

            _sut.Draw();

            Assert.IsTrue(countBeforeDraw > _sut.Count);

            Assert.IsTrue(countBeforeDraw == _sut.Count + 1);
        }

        [Test]
        public void CanDrawThree()
        {
            var countBeforeDraw = _sut.Count;

            _sut.Draw(3);

            Assert.IsTrue(countBeforeDraw > _sut.Count);

            Assert.IsTrue(countBeforeDraw == _sut.Count + 3);
        }

        [Test]
        public void FailsToDrawOnEmptyDeck()
        {
            while(!_sut.IsEmpty)
                _sut.Draw();

            Assert.IsTrue(_sut.Draw().Count == 0);
        }
    }
}