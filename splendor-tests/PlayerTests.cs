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
        Assert.IsTrue(_sut.TokenCount(TokenColor.Black) == 0);
        Assert.IsTrue(_sut.TokenCount(TokenColor.Blue) == 0);
        Assert.IsTrue(_sut.TokenCount(TokenColor.Green) == 0);
        Assert.IsTrue(_sut.TokenCount(TokenColor.Red) == 0);
        Assert.IsTrue(_sut.TokenCount(TokenColor.White) == 0);
        Assert.IsTrue(_sut.TokenCount(TokenColor.Yellow) == 0);
    }

    [Test]
    public void CanGetMoreTokens()
    {
        uint yellowCount = 2;
        uint blackCount = 1;
        _sut.CollectTokens(new TokenCollection(yellowCount: yellowCount, blackCount: blackCount));

        Assert.IsTrue(_sut.TokenCount(TokenColor.Yellow) == yellowCount);
        Assert.IsTrue(_sut.TokenCount(TokenColor.Black) == blackCount);
    }

    [Test]
    public void AcquiredDevelopmentsGenerateDiscounts()
    {
        var numberOfBlackDiscounts = 4;
        for (uint i = 0; i < numberOfBlackDiscounts; i++)
            _sut.BuyDevelopment(new Development(i, 0, TokenColor.Black, new TokenCollection()));

        Assert.AreEqual(numberOfBlackDiscounts, _sut.Discount(TokenColor.Black));
    }

    [Test]
    public void FailsToReserveWhenHandFull()
    {
        var price = new TokenCollection();
        Assert.IsTrue(_sut.TryReserve(new Development(1, 0, TokenColor.Black, price)));
        Assert.IsTrue(_sut.TryReserve(new Development(2, 0, TokenColor.Black, price)));
        Assert.IsTrue(_sut.TryReserve(new Development(3, 0, TokenColor.Black, price)));
        Assert.IsFalse(_sut.TryReserve(new Development(4, 0, TokenColor.Black, price)));
    }

    [Test]
    public void HasCorrectPrestigeScore()
    {
        var scored = new uint[] { 1, 2, 3 };

        foreach (var score in scored)
            _sut.BuyDevelopment(new Development(1, score, TokenColor.Black, new TokenCollection()));

        Assert.AreEqual(scored.ToList().Sum(e => e), _sut.Prestige);
    }

    [Test]
    public void CanConfirmHeCanBuyIfHasFundsWithoutGold()
    {
        uint blackCount = 3;
        uint blueCount = 2;

        _sut.CollectTokens(new TokenCollection(blackCount: blackCount, blueCount: blueCount));

        uint blackCost = 2;
        uint blueCost = 2;

        var cost = new TokenCollection(blackCount: blackCost, blueCount: blueCost);

        Assert.IsTrue(_sut.CanPay(cost));
    }

    [Test]
    public void CanConfirmHeCanBuyIfHasFundsWithGold()
    {
        uint blackCount = 1;
        uint blueCount = 2;
        uint yellowCount = 3;

        _sut.CollectTokens(new TokenCollection(blackCount: blackCount, blueCount: blueCount, yellowCount: yellowCount));

        uint blackCost = 3;
        uint blueCost = 3;
        var cost = new TokenCollection(blackCount: blackCost, blueCount: blueCost);

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
