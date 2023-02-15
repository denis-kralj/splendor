using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using splendor_lib;

namespace splendor_tests;

public class BuyDevelopmentActionTests
{
    private List<Development> _developments;
    private List<Noble> _nobles;

    [OneTimeSetUp]
    public void SetUp()
    {
        GameDataLoader gdl = new GameDataLoader();

        _developments = gdl.LoadDevelopments();
        _nobles = gdl.LoadNobles();
    }

    [Test]
    public void ShouldFailOnInvalidDevelopment()
    {
        var invalidDevelopment = new Development(11, 11, Token.Onyx, new TokenCollection());
        var sut = new BuyDevelopmentAction(invalidDevelopment);

        var buyer = new Player("Goku");

        var players = new List<Player>()
            {
                buyer,
                new Player("Vegeta"),
                new Player("Trunks")
            };

        var board = new GameBoard((PlayerCount)players.Count, _nobles, _developments);

        Assert.IsFalse(sut.TryExecuteAction(buyer, board, out var result));

        Assert.AreEqual(ExecutionResult.InvalidDevelopmentToBuy, result);
    }

    [Test]
    public void ShouldFailOnInsufficientTokens()
    {
        var buyer = new Player("Goku");

        var players = new List<Player>()
            {
                buyer,
                new Player("Vegeta"),
                new Player("Trunks")
            };

        var board = new GameBoard((PlayerCount)players.Count, _nobles, _developments);
        var developmentToBuy = board.PublicDevelopments.First();

        var sut = new BuyDevelopmentAction(developmentToBuy);
        Assert.IsFalse(sut.TryExecuteAction(buyer, board, out var result));

        Assert.AreEqual(ExecutionResult.InsufficientTokens, result);
    }

    [Test]
    public void ShouldSucceedWithSufficientTokensAndValidDevelopment()
    {
        var buyer = new Player("Goku");
        buyer.CollectTokens(new TokenCollection(9, 9, 9, 9, 9, 9));

        var players = new List<Player>()
            {
                buyer,
                new Player("Vegeta"),
                new Player("Trunks")
            };

        var board = new GameBoard((PlayerCount)players.Count, _nobles, _developments);
        var developmentToBuy = board.PublicDevelopments.First(d => d.Prestige > 1);

        var sut = new BuyDevelopmentAction(developmentToBuy);
        Assert.IsTrue(sut.TryExecuteAction(buyer, board, out var result));

        Assert.AreEqual(ExecutionResult.Success, result);

        Assert.AreEqual(buyer.Prestige, developmentToBuy.Prestige);
    }
}
