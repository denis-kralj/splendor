using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using splendor_lib;

namespace splendor_tests;

public class ReservePublicDevelopmentActionTests
{
    private IBoard _board;
    [SetUp]
    public void SetUp()
    {
        GameDataLoader gdl = new GameDataLoader();
        var developments = gdl.LoadDevelopments();
        _board = new GameBoard((PlayerCount)2, new List<Noble>(), developments);
    }

    [Test]
    public void ShouldAllowPlayerToReserve()
    {
        var reserver = new Player("Aang");
        var toReserve = _board.PublicDevelopments.First();
        var sut = new ReservePublicDevelopmentAction(toReserve);

        Assert.IsTrue(sut.TryExecuteAction(reserver, _board, out var result));
        Assert.AreEqual(ExecutionResult.Success, result);
    }

    [Test]
    public void ShouldGivePlayerANewReservedDevelopment()
    {
        var reserver = new Player("Aang");
        var expectedReservedCount = reserver.ReservedDevelopments.Count + 1;
        var toReserve = _board.PublicDevelopments.First();
        var sut = new ReservePublicDevelopmentAction(toReserve);

        sut.TryExecuteAction(reserver, _board, out var result);
        Assert.AreEqual(expectedReservedCount, reserver.ReservedDevelopments.Count);
    }

    [Test]
    public void ShouldGivePlayerOneGoldAfterSuccessfulReserve()
    {
        var reserver = new Player("Kora");
        var goldCountBefore = reserver.TokenCount(Token.Gold);
        var expectedGoldCount = ++goldCountBefore;
        var toReserve = _board.PublicDevelopments.First();
        var sut = new ReservePublicDevelopmentAction(toReserve);

        sut.TryExecuteAction(reserver, _board, out _);

        Assert.AreEqual(expectedGoldCount, reserver.TokenCount(Token.Gold));
    }

    [Test]
    public void ShouldGivePlayerNoGoldWhenNoGoldOnBoard()
    {
        var reserver = new Player("Katara");
        var goldCountBefore = reserver.TokenCount(Token.Gold);
        var toReserve = _board.PublicDevelopments.First();
        while (_board.TryTakeTokensFormBoard(new TokenCollection(goldCount: 1))) { }
        var sut = new ReservePublicDevelopmentAction(toReserve);

        sut.TryExecuteAction(reserver, _board, out _);

        Assert.AreEqual(goldCountBefore, reserver.TokenCount(Token.Gold));
    }

    [Test]
    public void ShouldAllowReserveWithNoGoldTokensOnBoard()
    {
        var reserver = new Player("Sokka");
        var goldCountBefore = reserver.TokenCount(Token.Gold);
        var toReserve = _board.PublicDevelopments.First();
        while (_board.TryTakeTokensFormBoard(new TokenCollection(goldCount: 1))) { }
        var sut = new ReservePublicDevelopmentAction(toReserve);

        Assert.IsTrue(sut.TryExecuteAction(reserver, _board, out var result));
        Assert.AreEqual(ExecutionResult.Success, result);
    }

    [Test]
    public void ShouldFailReserveIfDevelopmentNotOnBoard()
    {
        var reserver = new Player("Zuko");
        var toReserve = new Development(0, 0, Token.Gold, new TokenCollection());
        var sut = new ReservePublicDevelopmentAction(toReserve);

        Assert.IsFalse(sut.TryExecuteAction(reserver, _board, out var result));
        Assert.AreEqual(ExecutionResult.InvalidDevelopmentToReserve, result);
    }

    [Test]
    public void ShouldFailReserveIfPlayerHandFull()
    {
        var reserver = new Player("Zuko");
        var toReserve = _board.PublicDevelopments.First();
        reserver.TryReserve(new Development(0, 0, Token.Gold, new TokenCollection()));
        reserver.TryReserve(new Development(0, 0, Token.Gold, new TokenCollection()));
        reserver.TryReserve(new Development(0, 0, Token.Gold, new TokenCollection()));
        var sut = new ReservePublicDevelopmentAction(toReserve);

        Assert.IsFalse(sut.TryExecuteAction(reserver, _board, out var result));
        Assert.AreEqual(ExecutionResult.HandFull, result);
    }
}