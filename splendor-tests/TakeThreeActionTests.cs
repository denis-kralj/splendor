using System.Collections.Generic;
using NUnit.Framework;
using splendor_lib;

namespace splendor_tests;

public class TakeThreeActionTests
{
    private List<Noble> _nobles;
    private List<Development> _developments;

    [OneTimeSetUp]
    public void Setup()
    {
        GameDataLoader gdl = new GameDataLoader();

        _nobles = gdl.LoadNobles();
        _developments = gdl.LoadDevelopments();
    }

    [Test]
    public void CanExecuteInValidConditions()
    {
        var tokensToTake = new TokenCollection(1, 1, 1, 0, 0, 0);
        var player = new Player("Shaggy");
        var board = new GameBoard(PlayerCount.Two, _nobles, _developments);
        var sut = new TakeThreeAction(tokensToTake);

        Assert.IsTrue(sut.TryExecuteAction(player, board, out var result));
        Assert.AreEqual(ExecutionResult.Success, result);
    }

    [Test]
    public void CanExecuteWithLessThenThreeTokens()
    {
        var tokensToTake = new TokenCollection(1, 1, 0, 0, 0, 0);
        var player = new Player("Shaggy");
        var board = new GameBoard(PlayerCount.Two, _nobles, _developments);
        var sut = new TakeThreeAction(tokensToTake);

        Assert.IsTrue(sut.TryExecuteAction(player, board, out var result));
        Assert.AreEqual(ExecutionResult.Success, result);
    }

    [Test]
    public void DoesntExecuteWithInvalidTokenCount()
    {
        var tokensToTake = new TokenCollection(1, 1, 2, 0, 0, 0);
        var player = new Player("Shaggy");
        var board = new GameBoard(PlayerCount.Two, _nobles, _developments);
        var sut = new TakeThreeAction(tokensToTake);

        Assert.IsFalse(sut.TryExecuteAction(player, board, out var result));
        Assert.AreEqual(ExecutionResult.InvalidTokenCombination, result);
    }

    [Test]
    public void DoesntExecuteWithInsufficientTokensOnField()
    {
        var tokensToTake = new TokenCollection(1, 1, 1, 0, 0, 0);
        var player = new Player("Shaggy");
        var board = new GameBoard(PlayerCount.Two, _nobles, _developments);
        var sut = new TakeThreeAction(tokensToTake);
        board.TryTakeTokensFormBoard(new TokenCollection(4, 4, 4, 4, 4, 5));

        Assert.IsFalse(sut.TryExecuteAction(player, board, out var result));
        Assert.AreEqual(ExecutionResult.InsufficientTokens, result);
    }
}
