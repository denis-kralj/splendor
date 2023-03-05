using System.Collections.Generic;
using NUnit.Framework;
using splendor_lib;

namespace splendor_tests;

public class BuyPublicDevelopmentActionTests
{
    [Test]
    public void ShouldFailIfDevelopmentNotInPublicArea()
    {
        var developmentToBuy = new Development(0, 0, Token.Ruby, new TokenCollection());
        var board = new GameBoard(PlayerCount.Two, new List<Noble>(), new List<Development>());
        var player = new Player("Kratos");
        var sut = new BuyPublicDevelopmentAction(developmentToBuy);

        Assert.IsFalse(sut.TryExecuteAction(player, board, out var result));
        Assert.That(result, Is.EqualTo(ExecutionResult.InvalidDevelopmentToBuy));
    }

    [Test]
    public void ShouldFailIfPlayerCannotPayPrice()
    {
        var developmentToBuy = new Development(1, 0, Token.Ruby, new TokenCollection(1,1));
        var board = new GameBoard(PlayerCount.Two, new List<Noble>(), new List<Development>(){ developmentToBuy });
        var player = new Player("Atreus");
        var sut = new BuyPublicDevelopmentAction(developmentToBuy);

        Assert.IsFalse(sut.TryExecuteAction(player, board, out var result));
        Assert.That(result, Is.EqualTo(ExecutionResult.InsufficientTokens));
    }

    [Test]
    public void SucceedsToBuyExistingDevelopmentWithEnoughMoney()
    {
        var developmentToBuy = new Development(1, 0, Token.Ruby, new TokenCollection(1,1));
        var board = new GameBoard(PlayerCount.Two, new List<Noble>(), new List<Development>(){ developmentToBuy });
        var player = new Player("Freya");
        player.AddToken(Token.Diamond, 1);
        player.AddToken(Token.Onyx, 1);
        var sut = new BuyPublicDevelopmentAction(developmentToBuy);

        Assert.IsTrue(sut.TryExecuteAction(player, board, out var result));
        Assert.That(result, Is.EqualTo(ExecutionResult.Success));
    }

    [Test]
    public void RemovesTokensFromPlayer()
    {
        var developmentToBuy = new Development(1, 0, Token.Ruby, new TokenCollection(1,1,1,1));
        var board = new GameBoard(PlayerCount.Two, new List<Noble>(), new List<Development>(){ developmentToBuy });
        var player = new Player("Baldur");
        player.AddToken(Token.Diamond, 1);
        player.AddToken(Token.Onyx, 1);
        player.AddToken(Token.Sapphire, 1);
        player.AddToken(Token.Emerald, 1);
        var expectedDiamondTokens = player.GetTokenCount(Token.Diamond) - developmentToBuy.Cost.GetCount(Token.Diamond);
        var expectedOnyxTokens = player.GetTokenCount(Token.Onyx) - developmentToBuy.Cost.GetCount(Token.Onyx);
        var expectedSapphireTokens = player.GetTokenCount(Token.Sapphire) - developmentToBuy.Cost.GetCount(Token.Sapphire);
        var expectedEmeraldTokens = player.GetTokenCount(Token.Emerald) - developmentToBuy.Cost.GetCount(Token.Emerald);
        var sut = new BuyPublicDevelopmentAction(developmentToBuy);

        sut.TryExecuteAction(player, board, out var result);

        Assert.That(player.GetTokenCount(Token.Diamond), Is.EqualTo(expectedDiamondTokens));
        Assert.That(player.GetTokenCount(Token.Onyx), Is.EqualTo(expectedOnyxTokens));
        Assert.That(player.GetTokenCount(Token.Sapphire), Is.EqualTo(expectedSapphireTokens));
        Assert.That(player.GetTokenCount(Token.Emerald), Is.EqualTo(expectedEmeraldTokens));
    }

    [Test]
    public void AddsTokensToBoard()
    {
        var developmentToBuy = new Development(1, 0, Token.Ruby, new TokenCollection(1,1,1,1));
        var board = new GameBoard(PlayerCount.Two, new List<Noble>(), new List<Development>(){ developmentToBuy });
        var player = new Player("Thor");
        player.AddToken(Token.Diamond, 1);
        player.AddToken(Token.Onyx, 1);
        player.AddToken(Token.Sapphire, 1);
        player.AddToken(Token.Emerald, 1);
        var expectedDiamondTokens = board.GetTokenCount(Token.Diamond) + developmentToBuy.Cost.GetCount(Token.Diamond);
        var expectedOnyxTokens = board.GetTokenCount(Token.Onyx) + developmentToBuy.Cost.GetCount(Token.Onyx);
        var expectedSapphireTokens = board.GetTokenCount(Token.Sapphire) + developmentToBuy.Cost.GetCount(Token.Sapphire);
        var expectedEmeraldTokens = board.GetTokenCount(Token.Emerald) + developmentToBuy.Cost.GetCount(Token.Emerald);
        var sut = new BuyPublicDevelopmentAction(developmentToBuy);

        sut.TryExecuteAction(player, board, out var result);

        Assert.That(board.GetTokenCount(Token.Diamond), Is.EqualTo(expectedDiamondTokens));
        Assert.That(board.GetTokenCount(Token.Onyx), Is.EqualTo(expectedOnyxTokens));
        Assert.That(board.GetTokenCount(Token.Sapphire), Is.EqualTo(expectedSapphireTokens));
        Assert.That(board.GetTokenCount(Token.Emerald), Is.EqualTo(expectedEmeraldTokens));
    }

    [Test]
    public void CalculatesDiscountsCorrectly()
    {
        var developmentToBuy = new Development(1, 0, Token.Ruby, new TokenCollection(1));
        var developmentThatDiscountsDiamondCosts = new Development(0, 0, Token.Diamond, new TokenCollection());
        var board = new GameBoard(PlayerCount.Two, new List<Noble>(), new List<Development>(){ developmentToBuy });
        var player = new Player("Odin");
        player.BuyDevelopment(developmentThatDiscountsDiamondCosts);
        player.AddToken(Token.Diamond, 1);
        var expectedDiamondTokens = player.GetTokenCount(Token.Diamond);
        var sut = new BuyPublicDevelopmentAction(developmentToBuy);

        sut.TryExecuteAction(player, board, out var result);

        Assert.That(player.GetTokenCount(Token.Diamond), Is.EqualTo(expectedDiamondTokens));
    }

    [Test]
    public void RemovesGoldWhenNeeded()
    {
        var developmentToBuy = new Development(1, 0, Token.Ruby, new TokenCollection(1));
        var board = new GameBoard(PlayerCount.Two, new List<Noble>(), new List<Development>(){ developmentToBuy });
        var player = new Player("Zeus");
        player.AddToken(Token.Gold, 1);
        var expectedGoldTokens = player.GetTokenCount(Token.Gold) - developmentToBuy.Cost.TotalTokens;
        var sut = new BuyPublicDevelopmentAction(developmentToBuy);

        sut.TryExecuteAction(player, board, out var result);

        Assert.That(player.GetTokenCount(Token.Gold), Is.EqualTo(expectedGoldTokens));
    }
}