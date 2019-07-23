using NUnit.Framework;
using splendor_lib;

namespace splendor_tests
{
    public class GameBoardTests
    {
        private const string pathToDevelopments = @"csv-data\developments-data.csv";
        private const string pathToNobles = @"csv-data\nobles-data.csv";
        private readonly GameDataLoader gdl = new GameDataLoader(pathToDevelopments, pathToNobles);
        private GameBoard _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new GameBoard(PlayerCount.Four, gdl.LoadNobles(), gdl.LoadDevelopments());
        }

        [Test]
        public void InitializesCorrectly()
        {
            var playerCount = 1;

            while (playerCount++ < 5)
            {
                _sut.SetupBoard((PlayerCount)playerCount, gdl.LoadNobles(), gdl.LoadDevelopments());
                Assert.AreEqual(playerCount, _sut.BoardNobles.Count - 1);
            }
        }

        [Test]
        public void HasInitialTokens()
        {
            var playerCount = 1;

            while (playerCount++ < 5)
            {
                _sut.SetupBoard((PlayerCount)playerCount, gdl.LoadNobles(), gdl.LoadDevelopments());
                var tokens = _sut.BoardTokens;
                TokenCollection expectedTokens = null;
                switch (playerCount)
                {
                    case 4:
                        expectedTokens = new TokenCollection(7, 7, 7, 7, 7, 5);
                        break;
                    case 3:
                        expectedTokens = new TokenCollection(5, 5, 5, 5, 5, 5);
                        break;
                    case 2:
                    default:
                        expectedTokens = new TokenCollection(4, 4, 4, 4, 4, 5);
                        break;
                }
                Assert.AreEqual(expectedTokens, _sut.BoardTokens);
            }
        }

        [Test]
        public void CanRecieveTokens()
        {
            uint yellowCount = 2;
            uint redCount = 1;
            uint greenCount = 6;
            uint blackCount = 1;
            uint blueCount = 0;
            uint whiteCount = 2;
            var tokensToReturnToBoard = new TokenCollection(whiteCount, blackCount, blueCount, greenCount, redCount, yellowCount);

            var expectedTokens = new TokenCollection(7 + whiteCount, 7 + blackCount, 7 + blueCount, 7 + greenCount, 7 + redCount, 5 + yellowCount);

            _sut.RecieveTokens(tokensToReturnToBoard);

            var actualTokens = _sut.BoardTokens;

            Assert.AreEqual(expectedTokens, actualTokens);
        }
    }
}