using NUnit.Framework;
using splendor_lib;

namespace splendor_tests
{
    public class PlayerCircleTests
    {
        private PlayerCircle _sut;
        private string[] players = new [] { "Ron", "Jerry", "April" };
        [SetUp]
        public void Setup()
        {
            _sut = new PlayerCircle(players);
        }

        [Test]
        public void FirstPlayerIsCurrent()
        {
            Assert.IsTrue(_sut.Current.PlayerName == players[0]);
        }

        [Test]
        public void SecondPlayerIsCurrentAfterOnePass()
        {
            _sut.Pass();
            Assert.IsTrue(_sut.Current.PlayerName == players[1]);
        }

        [Test]
        public void ThirdPlayerIsCurrentAfterTwoPasses()
        {
            _sut.Pass();
            _sut.Pass();
            Assert.IsTrue(_sut.Current.PlayerName == players[2]);
        }

        [Test]
        public void LastPlayerIsDetected()
        {
            _sut.Pass();
            _sut.Pass();
            Assert.IsTrue(_sut.LastPlayersTurn);
        }

        [Test]
        public void CirclesBackAfterReachingEndOfPlayerList()
        {
            _sut.Pass();
            _sut.Pass();
            _sut.Pass();
            Assert.IsTrue(_sut.Current.PlayerName == players[0]);
        }
    }
}