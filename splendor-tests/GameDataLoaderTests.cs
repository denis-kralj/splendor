using NUnit.Framework;
using splendor_lib;
using System.Linq;
using System.Collections.Generic;

namespace splendor_tests;

public class GameDataLoaderTests
{
    private List<Development> _sutAllDevelopments;
    private List<Noble> _sutAllNobles;

    [SetUp]
    public void Setup()
    {

        var _sut = new GameDataLoader();

        _sutAllDevelopments = _sut.LoadDevelopments();
        _sutAllNobles = _sut.LoadNobles();
    }

    [Test]
    public void LoadsAllDevelopmentCards()
    {
        Assert.AreEqual(90, _sutAllDevelopments.Count);
    }

    [Test]
    public void Loads40Level1Cards()
    {
        var allLevel1Cards = _sutAllDevelopments.Where(c => c.Level == 1).ToList();
        Assert.AreEqual(40, allLevel1Cards.Count);
    }

    [Test]
    public void Loads30Level2Cards()
    {
        var allLevel2Cards = _sutAllDevelopments.Where(c => c.Level == 2).ToList();
        Assert.AreEqual(30, allLevel2Cards.Count);
    }

    [Test]
    public void Loads20Level3Cards()
    {
        var allLevel3Cards = _sutAllDevelopments.Where(c => c.Level == 3).ToList();
        Assert.AreEqual(20, allLevel3Cards.Count);
    }

    [Test]
    public void LoadsOnlyUniqueCards()
    {
        Assert.AreEqual(_sutAllDevelopments.Count, _sutAllDevelopments.Distinct().Count());
    }

    public void LoadAllNobleCards()
    {
        Assert.AreEqual(10, _sutAllNobles.Count);
    }

    public void AllNobleCardsAreDistinct()
    {
        Assert.AreEqual(_sutAllNobles.Count, _sutAllNobles.Distinct().Count());
    }
}
