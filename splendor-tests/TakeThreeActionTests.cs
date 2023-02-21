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
        var player = new Player("Shaggy");
        var board = new GameBoard(PlayerCount.Two, _nobles, _developments);
        var sut = new TakeThreeAction(Token.Diamond, Token.Onyx, Token.Sapphire);

        Assert.IsTrue(sut.TryExecuteAction(player, board, out var result));
        Assert.AreEqual(ExecutionResult.Success, result);
    }

    [Test]
    public void CanExecuteWithLessThenThreeTokens()
    {
        var player = new Player("Shaggy");
        var board = new GameBoard(PlayerCount.Two, _nobles, _developments);
        var sut = new TakeThreeAction(Token.Diamond, Token.Onyx);

        Assert.IsTrue(sut.TryExecuteAction(player, board, out var result));
        Assert.AreEqual(ExecutionResult.Success, result);
    }

    [Test]
    public void DoesntExecuteWithInvalidTokenCount()
    {
        var player = new Player("Shaggy");
        var board = new GameBoard(PlayerCount.Two, _nobles, _developments);
        var sut = new TakeThreeAction(Token.Diamond, Token.Onyx, Token.Sapphire, Token.Sapphire);

        Assert.IsFalse(sut.TryExecuteAction(player, board, out var result));
        Assert.AreEqual(ExecutionResult.InvalidTokenCombination, result);
    }

    [Test]
    public void DoesntExecuteWithInsufficientTokensOnField()
    {
        var player = new Player("Shaggy");
        var board = new GameBoard(PlayerCount.Two, _nobles, _developments);
        var sut = new TakeThreeAction(Token.Diamond, Token.Onyx, Token.Sapphire);
        board.RemoveAllTokens();

        Assert.IsFalse(sut.TryExecuteAction(player, board, out var result));
        Assert.AreEqual(ExecutionResult.InsufficientTokens, result);
    }
}
