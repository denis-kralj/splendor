using NUnit.Framework;
using splendor_lib;

namespace splendor_tests;

public class NobleTests
{
    private Noble _sut;
    private const uint blue = 3;
    private const uint white = 3;
    private const uint black = 3;

    [SetUp]
    public void SetUp()
    {
        _sut = new Noble(3, new NobleRequirements(new TokenCollection(white, black, blue)));
    }

    [Test]
    public void HasRightRequirements()
    {
        uint expectedRedRequirement = 0, expectedGreenRequirement = 0;

        Assert.AreEqual(blue, _sut.Requirements.Cost(TokenColor.Blue));
        Assert.AreEqual(white, _sut.Requirements.Cost(TokenColor.White));
        Assert.AreEqual(black, _sut.Requirements.Cost(TokenColor.Black));
        Assert.AreEqual(expectedGreenRequirement, _sut.Requirements.Cost(TokenColor.Green));
        Assert.AreEqual(expectedRedRequirement, _sut.Requirements.Cost(TokenColor.Red));
    }

    [Test]
    public void WillNotVisitPlayerThatDoesntCoverRequirements()
    {
        var player = new Player("A name");

        Assert.IsFalse(_sut.TryVisit(player));
    }

    [Test]
    public void WillVisitPlayerThatDoesCoverRequirements()
    {
        var player = new Player("A name");

        for (int i = 0; i < black; i++)
            player.BuyDevelopment(new Development(0, 0, TokenColor.Black, new TokenCollection()));
        for (int i = 0; i < blue; i++)
            player.BuyDevelopment(new Development(0, 0, TokenColor.Blue, new TokenCollection()));
        for (int i = 0; i < white; i++)
            player.BuyDevelopment(new Development(0, 0, TokenColor.White, new TokenCollection()));

        Assert.IsTrue(_sut.TryVisit(player));
    }

    [Test]
    public void CanDetectIfAPlayerIsEligible()
    {
        var player = new Player("A name");
        Assert.IsFalse(_sut.CanVisit(player));
    }
}
