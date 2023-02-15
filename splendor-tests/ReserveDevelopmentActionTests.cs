using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using splendor_lib;

namespace splendor_tests;

public class ReserveDevelopmentActionTests
{
    private List<Development> _developments;

    [OneTimeSetUp]
    public void SetUp()
    {
        GameDataLoader gdl = new GameDataLoader();

        _developments = gdl.LoadDevelopments();
    }

    [Test]
    public void ShouldReserveDevelopmentFromPublicArea()
    {
        var reserver = new Player("Goku");

        var players = new List<Player>()
            {
                reserver,
                new Player("Vegeta"),
                new Player("Trunks")
            };

        var board = new GameBoard((PlayerCount)players.Count, new List<Noble>(), _developments);

        var toReserve = board.PublicDevelopments.First();

        var sut = new ReserveDevelopmentAction(Location.Public, toReserve);

        Assert.IsTrue(sut.TryExecuteAction(reserver, board, out var result));

        Assert.AreEqual(ExecutionResult.Success, result);
    }

    [Test]
    public void ShouldNotReserveWhenHandFull()
    {
        var reserver = new Player("Goku");

        reserver.TryReserve(new Development(1, 1, TokenColor.Red, new TokenCollection()));
        reserver.TryReserve(new Development(1, 1, TokenColor.Red, new TokenCollection()));
        reserver.TryReserve(new Development(1, 1, TokenColor.Red, new TokenCollection()));

        var players = new List<Player>()
            {
                reserver,
                new Player("Vegeta"),
                new Player("Trunks")
            };

        var board = new GameBoard((PlayerCount)players.Count, new List<Noble>(), _developments);

        var toReserve = board.PublicDevelopments.First();

        var sut = new ReserveDevelopmentAction(Location.Public, toReserve);

        Assert.IsFalse(sut.TryExecuteAction(reserver, board, out var result));

        Assert.AreEqual(ExecutionResult.HandFull, result);
    }

    [Test]
    public void ShouldNotReserveIfNotInPublicSpace()
    {
        var reserver = new Player("Goku");
        var fakeDevelopment = new Development(1, 1, TokenColor.Red, new TokenCollection());

        var players = new List<Player>()
            {
                reserver,
                new Player("Vegeta"),
                new Player("Trunks")
            };

        var board = new GameBoard((PlayerCount)players.Count, new List<Noble>(), _developments);

        var sut = new ReserveDevelopmentAction(Location.Public, fakeDevelopment);

        Assert.IsFalse(sut.TryExecuteAction(reserver, board, out var result));

        Assert.AreEqual(ExecutionResult.InvalidDevelopmentToReserve, result);
    }

    [Test]
    public void ShouldReserveFromDeck()
    {
        var reserver = new Player("Goku");

        var players = new List<Player>()
            {
                reserver,
                new Player("Vegeta"),
                new Player("Trunks")
            };

        var board = new GameBoard((PlayerCount)players.Count, new List<Noble>(), _developments);

        var sut = new ReserveDevelopmentAction(Location.Level2Deck, null);

        Assert.IsTrue(sut.TryExecuteAction(reserver, board, out var result));

        Assert.AreEqual(ExecutionResult.Success, result);

        Assert.AreEqual(1, reserver.ReservedDevelopments.Count);

        Assert.AreEqual(2, reserver.ReservedDevelopments.First().Level);
    }
}
