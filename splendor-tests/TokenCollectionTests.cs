using NUnit.Framework;
using splendor_lib;

namespace splendor_tests;

public class TokenCollectionTests
{
    private TokenCollection _sut;

    [SetUp]
    public void Setup()
    {
        _sut = new TokenCollection();
    }

    [Test]
    public void ShouldAddTokensToCollectionByFirstOverride()
    {
        uint tokenCountToAdd = 3;
        var countBefore = _sut.TotalTokens;

        _sut.AddTokens(new TokenCollection(diamondCount: tokenCountToAdd));

        Assert.AreEqual(countBefore + tokenCountToAdd, _sut.TotalTokens);
    }

    [Test]
    public void ShouldAddTokensToCollectionBySecondOverride()
    {
        uint tokenCountToAdd = 5;
        var countBefore = _sut.TotalTokens;

        _sut.AddTokens(Token.Emerald, tokenCountToAdd);

        Assert.AreEqual(countBefore + tokenCountToAdd, tokenCountToAdd);
    }

    [Test]
    public void ShouldRemoveTokens()
    {
        uint blackAddCount = 2;
        uint blueAddCount = 1;
        uint blackRemoveCount = 1;
        uint blueRemoveCount = 1;
        uint totalBeforeAdd = _sut.TotalTokens;

        _sut.AddTokens(new TokenCollection(onyxCount: blackAddCount, sapphireCount: blueAddCount));

        var totalAfterAdd = totalBeforeAdd + blackAddCount + blueAddCount;

        Assert.AreEqual(totalAfterAdd, _sut.TotalTokens);

        Assert.IsTrue(_sut.TryTake(new TokenCollection(onyxCount: blackRemoveCount, sapphireCount: blueRemoveCount)));

        var totalAfterTake = totalAfterAdd - blackRemoveCount - blueRemoveCount;

        Assert.AreEqual(totalAfterTake, _sut.TotalTokens);
    }

    [Test]
    public void ShouldFailTryWhenRemovingTokensItDoesNotHave()
    {
        uint redState = 0;
        _sut = new TokenCollection(rubyCount: redState);

        Assert.IsFalse(_sut.TryTake(Token.Ruby, redState + 1));
        Assert.IsFalse(_sut.TryTake(new TokenCollection(rubyCount: redState + 2)));
    }

    [Test]
    public void CanCountSpecificTokenTypeContained()
    {
        uint blackCount = 2;
        uint blueCount = 1;
        uint greenCount = 0;
        uint redCount = 2;
        uint whiteCount = 0;
        uint yellowCount = 1;

        _sut.AddTokens(new TokenCollection(whiteCount, blackCount, blueCount, greenCount, redCount, yellowCount));

        Assert.AreEqual(_sut.GetCount(Token.Onyx), blackCount);
        Assert.AreEqual(_sut.GetCount(Token.Sapphire), blueCount);
        Assert.AreEqual(_sut.GetCount(Token.Emerald), greenCount);
        Assert.AreEqual(_sut.GetCount(Token.Ruby), redCount);
        Assert.AreEqual(_sut.GetCount(Token.Diamond), whiteCount);
        Assert.AreEqual(_sut.GetCount(Token.Gold), yellowCount);
    }

    [Test]
    public void ShouldSetupCollection()
    {
        uint whiteCount = 3;
        uint blackCount = 5;
        uint blueCount = 1;
        uint greenCount = 0;
        uint redCount = 11;
        uint yellowCount = 6;

        _sut = new TokenCollection(whiteCount, blackCount, blueCount, greenCount, redCount, yellowCount);

        Assert.AreEqual(_sut.GetCount(Token.Onyx), blackCount);
        Assert.AreEqual(_sut.GetCount(Token.Sapphire), blueCount);
        Assert.AreEqual(_sut.GetCount(Token.Emerald), greenCount);
        Assert.AreEqual(_sut.GetCount(Token.Ruby), redCount);
        Assert.AreEqual(_sut.GetCount(Token.Diamond), whiteCount);
        Assert.AreEqual(_sut.GetCount(Token.Gold), yellowCount);
    }
}
