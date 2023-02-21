using System.Collections.Generic;
using NUnit.Framework;
using splendor_lib;

namespace splendor_tests;

public class TakeTwoSameActionTests
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
        var tokensToTake = new TokenCollection(0, 0, 2, 0, 0, 0);
        var player = new Player("Shaggy");
        var board = new GameBoard(PlayerCount.Two, _nobles, _developments);
        var sut = new TakeTwoSameAction(Token.Ruby);

        Assert.IsTrue(sut.TryExecuteAction(player, board, out var result));
        Assert.AreEqual(ExecutionResult.Success, result);
    }

    [Test]
    public void DoesntExecuteWithInsufficientTokensOnField()
    {
        var tokensToTake = new TokenCollection(0, 0, 2, 0, 0, 0);
        var player = new Player("Shaggy");
        var board = new GameBoard(PlayerCount.Two, _nobles, _developments);
        var sut = new TakeTwoSameAction(Token.Sapphire);
        board.RemoveAllTokens();

        Assert.IsFalse(sut.TryExecuteAction(player, board, out var result));
        Assert.AreEqual(ExecutionResult.InsufficientTokens, result);
    }

    [Test]
    public void DoesntExecuteWhenRequestedColorHasLessThenFourOnBoard()
    {
        var tokensToTake = new TokenCollection(0, 0, 2, 0, 0, 0);
        var player = new Player("Shaggy");
        var board = new GameBoard(PlayerCount.Two, _nobles, _developments);
        var sut = new TakeTwoSameAction(Token.Sapphire);
        board.RemoveAllTokensOfType(Token.Sapphire);

        Assert.IsFalse(sut.TryExecuteAction(player, board, out var result));
        Assert.AreEqual(ExecutionResult.InsufficientTokens, result);
    }
}
