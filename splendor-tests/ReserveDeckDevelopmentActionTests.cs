using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using splendor_lib;

namespace splendor_tests;

public class ReserveDeckDevelopmentActionTests
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
        var sut = new ReserveDeckDevelopmentAction(DevelopmentDeck.Level1);

        Assert.IsTrue(sut.TryExecuteAction(reserver, _board, out var result));
        Assert.AreEqual(ExecutionResult.Success, result);
    }

    [Test]
    public void ShouldGivePlayerANewDevelopment()
    {
        var reserver = new Player("Aang");
        var expectedReservedCount = reserver.ReservedDevelopments.Count + 1;
        var sut = new ReserveDeckDevelopmentAction(DevelopmentDeck.Level1);

        sut.TryExecuteAction(reserver, _board, out var result);
        Assert.AreEqual(expectedReservedCount, reserver.ReservedDevelopments.Count);
    }

    [Test]
    public void ShouldDrawFromLevelOneDeck()
    {
        var reserver = new Player("Aang");
        var sut = new ReserveDeckDevelopmentAction(DevelopmentDeck.Level1);
        sut.TryExecuteAction(reserver, _board, out _);

        Assert.AreEqual(1, reserver.ReservedDevelopments.First().Level);
    }

    [Test]
    public void ShouldDrawFromLevelTwoDeck()
    {
        var reserver = new Player("Aang");
        var sut = new ReserveDeckDevelopmentAction(DevelopmentDeck.Level2);
        sut.TryExecuteAction(reserver, _board, out _);

        Assert.AreEqual(2, reserver.ReservedDevelopments.First().Level);
    }

    [Test]
    public void ShouldDrawFromLevelThreeDeck()
    {
        var reserver = new Player("Aang");
        var sut = new ReserveDeckDevelopmentAction(DevelopmentDeck.Level3);
        sut.TryExecuteAction(reserver, _board, out _);

        Assert.AreEqual(3, reserver.ReservedDevelopments.First().Level);
    }

    [Test]
    public void ShouldGivePlayerOneGoldAfterSuccessfulReserve()
    {
        var reserver = new Player("Kora");
        var goldCountBefore = reserver.GetTokenCount(Token.Gold);
        var expectedGoldCount = ++goldCountBefore;
        var sut = new ReserveDeckDevelopmentAction(DevelopmentDeck.Level1);

        sut.TryExecuteAction(reserver, _board, out _);

        Assert.AreEqual(expectedGoldCount, reserver.GetTokenCount(Token.Gold));
    }

    [Test]
    public void ShouldGivePlayerNoGoldWhenNoGoldOnBoard()
    {
        var reserver = new Player("Katara");
        var goldCountBefore = reserver.GetTokenCount(Token.Gold);
        _board.RemoveAllTokensOfType(Token.Gold);
        var sut = new ReserveDeckDevelopmentAction(DevelopmentDeck.Level1);

        sut.TryExecuteAction(reserver, _board, out _);

        Assert.AreEqual(goldCountBefore, reserver.GetTokenCount(Token.Gold));
    }

    [Test]
    public void ShouldAllowReserveWithNoGoldTokensOnBoard()
    {
        var reserver = new Player("Sokka");
        var goldCountBefore = reserver.GetTokenCount(Token.Gold);
        _board.RemoveAllTokensOfType(Token.Gold);
        var sut = new ReserveDeckDevelopmentAction(DevelopmentDeck.Level1);

        Assert.IsTrue(sut.TryExecuteAction(reserver, _board, out var result));
        Assert.AreEqual(ExecutionResult.Success, result);
    }

    [Test]
    public void ShouldFailReserveIfDeckIsEmpty()
    {
        var reserver = new Player("Zuko");
        _board = new GameBoard((PlayerCount)2, new List<Noble>(), new List<Development>());
        var sut = new ReserveDeckDevelopmentAction(DevelopmentDeck.Level1);

        Assert.IsFalse(sut.TryExecuteAction(reserver, _board, out var result));
        Assert.AreEqual(ExecutionResult.CantDraw, result);
    }

    [Test]
    public void ShouldFailReserveIfPlayerHandFull()
    {
        var reserver = new Player("Zuko");
        reserver.TryReserve(new Development(0, 0, Token.Gold, new TokenCollection()));
        reserver.TryReserve(new Development(0, 0, Token.Gold, new TokenCollection()));
        reserver.TryReserve(new Development(0, 0, Token.Gold, new TokenCollection()));
        var sut = new ReserveDeckDevelopmentAction(DevelopmentDeck.Level1);

        Assert.IsFalse(sut.TryExecuteAction(reserver, _board, out var result));
        Assert.AreEqual(ExecutionResult.HandFull, result);
    }
}
