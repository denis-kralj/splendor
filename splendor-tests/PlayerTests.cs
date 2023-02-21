using System.Linq;
using NUnit.Framework;
using splendor_lib;

namespace splendor_tests;

public class PlayerTests
{
    private Player _sut;

    [SetUp]
    public void Setup()
    {
        _sut = new Player("A name");
    }

    [Test]
    public void PlayerIsInitializedWithoutTokens()
    {
        Assert.IsTrue(_sut.GetTokenCount(Token.Onyx) == 0);
        Assert.IsTrue(_sut.GetTokenCount(Token.Sapphire) == 0);
        Assert.IsTrue(_sut.GetTokenCount(Token.Emerald) == 0);
        Assert.IsTrue(_sut.GetTokenCount(Token.Ruby) == 0);
        Assert.IsTrue(_sut.GetTokenCount(Token.Diamond) == 0);
        Assert.IsTrue(_sut.GetTokenCount(Token.Gold) == 0);
    }

    [Test]
    public void CanGetMoreTokens()
    {
        uint yellowCount = 2;
        uint blackCount = 1;
        _sut.AddToken(Token.Gold, yellowCount);
        _sut.AddToken(Token.Onyx, blackCount);

        Assert.IsTrue(_sut.GetTokenCount(Token.Gold) == yellowCount);
        Assert.IsTrue(_sut.GetTokenCount(Token.Onyx) == blackCount);
    }

    [Test]
    public void AcquiredDevelopmentsGenerateDiscounts()
    {
        var numberOfBlackDiscounts = 4;
        for (uint i = 0; i < numberOfBlackDiscounts; i++)
            _sut.BuyDevelopment(new Development(i, 0, Token.Onyx, new TokenCollection()));

        Assert.AreEqual(numberOfBlackDiscounts, _sut.Discount(Token.Onyx));
    }

    [Test]
    public void FailsToReserveWhenHandFull()
    {
        var price = new TokenCollection();
        Assert.IsTrue(_sut.TryReserve(new Development(1, 0, Token.Onyx, price)));
        Assert.IsTrue(_sut.TryReserve(new Development(2, 0, Token.Onyx, price)));
        Assert.IsTrue(_sut.TryReserve(new Development(3, 0, Token.Onyx, price)));
        Assert.IsFalse(_sut.TryReserve(new Development(4, 0, Token.Onyx, price)));
    }

    [Test]
    public void HasCorrectPrestigeScore()
    {
        var scored = new uint[] { 1, 2, 3 };

        foreach (var score in scored)
            _sut.BuyDevelopment(new Development(1, score, Token.Onyx, new TokenCollection()));

        Assert.AreEqual(scored.ToList().Sum(e => e), _sut.Prestige);
    }

    [Test]
    public void CanConfirmHeCanBuyIfHasFundsWithoutGold()
    {
        _sut.AddToken(Token.Onyx, 3);
        _sut.AddToken(Token.Sapphire, 2);

        uint blackCost = 2;
        uint blueCost = 2;

        var cost = new TokenCollection(onyxCount: blackCost, sapphireCount: blueCost);

        Assert.IsTrue(_sut.CanPay(cost));
    }

    [Test]
    public void CanConfirmHeCanBuyIfHasFundsWithGold()
    {
        _sut.AddToken(Token.Onyx, 1);
        _sut.AddToken(Token.Sapphire, 2);
        _sut.AddToken(Token.Gold, 3);

        uint blackCost = 3;
        uint blueCost = 3;
        var cost = new TokenCollection(onyxCount: blackCost, sapphireCount: blueCost);

        Assert.IsTrue(_sut.CanPay(cost));
    }

    [Test]
    public void CalculatesPrestigeWithTakenNobles()
    {
        uint noblePrestige = 2;

        var noble = new Noble(noblePrestige, new NobleRequirements(new TokenCollection()));
        _sut.TakeNoble(noble);

        Assert.AreEqual(_sut.Prestige, noblePrestige);
    }
}
