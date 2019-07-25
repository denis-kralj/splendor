using NUnit.Framework;
using splendor_lib;

namespace splendor_tests
{
    public class NobleTests
    {
        private Noble _sut;
        private const uint blue = 3;
        private const uint white = 3;
        private const uint black = 3;

        [SetUp]
        public void SetUp()
        {
            _sut = new Noble(3, new TokenCollection(white, black, blue));
        }

        [Test]
        public void HasRightRequirements()
        {
            uint expectedRedReqirement = 0, expectedGreenReqirement = 0;

            Assert.AreEqual(blue, _sut.Requirements.Cost(Token.Blue));
            Assert.AreEqual(white, _sut.Requirements.Cost(Token.White));
            Assert.AreEqual(black, _sut.Requirements.Cost(Token.Black));
            Assert.AreEqual(expectedGreenReqirement, _sut.Requirements.Cost(Token.Green));
            Assert.AreEqual(expectedRedReqirement, _sut.Requirements.Cost(Token.Red));
        }

        [Test]
        public void WillNotVisitPlayerThatDoesntCoverRequirements()
        {
            var player = new Player("A name");

            Assert.IsFalse(_sut.TryVisit(player));
        }

        [Test]
        public void WillVisitPlayerThatDoesCoverRequirements()
        {
            var player = new Player("A name");

            for(int i = 0; i < black; i++)
                player.GetDevelopment(new Development(0,0,Token.Black,new TokenCollection()));
            for(int i = 0; i < blue; i++)
                player.GetDevelopment(new Development(0,0,Token.Blue,new TokenCollection()));
            for(int i = 0; i < white; i++)
                player.GetDevelopment(new Development(0,0,Token.White,new TokenCollection()));

            Assert.IsTrue(_sut.TryVisit(player));
        }
    }
}