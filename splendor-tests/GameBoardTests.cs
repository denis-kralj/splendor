using System.Linq;
using NUnit.Framework;
using splendor_lib;

namespace splendor_tests;

public class GameBoardTests
{
    private readonly GameDataLoader gdl = new GameDataLoader();
    private GameBoard _sut;

    [SetUp]
    public void Setup()
    {
        _sut = new GameBoard(PlayerCount.Four, gdl.LoadNobles(), gdl.LoadDevelopments());
    }

    [Test]
    public void HasCorrectCountOfNoblesBasedOnPlayerCount()
    {
        var playerCount = 1;

        while (playerCount++ < 5)
        {
            _sut = new GameBoard((PlayerCount)playerCount, gdl.LoadNobles(), gdl.LoadDevelopments());
            Assert.AreEqual(playerCount, _sut.BoardNobles.Count - 1);
        }
    }

    [Test]
    public void HasCorrectInitialTokensBasedOnPlayerCount()
    {
        var playerCount = 1;

        while (playerCount++ < 5)
        {
            _sut = new GameBoard((PlayerCount)playerCount, gdl.LoadNobles(), gdl.LoadDevelopments());
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
    public void CanReceiveTokens()
    {
        uint yellowCount = 2;
        uint redCount = 1;
        uint greenCount = 6;
        uint blackCount = 1;
        uint blueCount = 0;
        uint whiteCount = 2;

        var expectedTokens = new TokenCollection(7 + whiteCount, 7 + blackCount, 7 + blueCount, 7 + greenCount, 7 + redCount, 5 + yellowCount);

        _sut.AddToken(Token.Gold, yellowCount);
        _sut.AddToken(Token.Diamond, whiteCount);
        _sut.AddToken(Token.Emerald, greenCount);
        _sut.AddToken(Token.Onyx, blackCount);
        _sut.AddToken(Token.Ruby, redCount);
        _sut.AddToken(Token.Sapphire, blueCount);

        var actualTokens = _sut.BoardTokens;

        Assert.AreEqual(expectedTokens, actualTokens);
    }

    [Test]
    public void CanGivePublicDevelopmentsAsList()
    {
        var allDevelopments = _sut.PublicDevelopments;

        Assert.NotNull(allDevelopments);
        Assert.AreEqual(12, allDevelopments.Count);
    }

    [Test]
    public void CanTakeDevelopmentFromPublic()
    {
        var toReserve = _sut.PublicDevelopments.Last();

        _sut.TryRemoveDevelopment(Location.Public, toReserve, out var actuallyTaken);

        Assert.IsFalse(_sut.PublicDevelopments.Contains(actuallyTaken));
    }

    [Test]
    public void CanTakeDevelopmentFromDeck()
    {
        _sut.TryRemoveDevelopment(Location.Level1Deck, null, out var actuallyTaken);

        Assert.NotNull(actuallyTaken);
        Assert.AreEqual(1, actuallyTaken.Level);
    }
}
