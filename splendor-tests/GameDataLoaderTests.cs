using NUnit.Framework;
using splendor_lib;
using System.Linq;

namespace Tests
{
    public class GameDataLoaderTests
    {
        private GameDataLoader _sut;

        [SetUp]
        public void Setup()
        {
            var pathToDevelopments = @"..\..\..\..\cvs-data\developments-data.csv";
            var pathToNobles = @"..\..\..\..\..\cvs-data\nobles-data.csv";

            _sut = new GameDataLoader(pathToDevelopments, pathToNobles);
        }

        [Test]
        public void LoadsAllDevelopmentCards()
        {
            //act
            var allDevelopmentCards = _sut.LoadDevelopments();
            //assert
            Assert.AreEqual(90, allDevelopmentCards.Count);
        }

        [Test]
        public void Loads40Level1Cards()
        {
            //act
            var allDevelopmentCards = _sut.LoadDevelopments();
            var allLevel1Cards = allDevelopmentCards.Where(c => c.Level == 1).ToList();
            Assert.AreEqual(40, allLevel1Cards.Count);
        }

        [Test]
        public void Loads30Level2Cards()
        {
            //act
            var allDevelopmentCards = _sut.LoadDevelopments();
            var allLevel2Cards = allDevelopmentCards.Where(c => c.Level == 2).ToList();
            Assert.AreEqual(30, allLevel2Cards.Count);
        }

        [Test]
        public void Loads20Level3Cards()
        {
            //act
            var allDevelopmentCards = _sut.LoadDevelopments();
            var allLevel3Cards = allDevelopmentCards.Where(c => c.Level == 3).ToList();
            Assert.AreEqual(20, allLevel3Cards.Count);
        }

        [Test]
        public void LoadsOnlyUniqueCards()
        {
            var allDevelopmentCards = _sut.LoadDevelopments();
            Assert.True(allDevelopmentCards.Count == allDevelopmentCards.Distinct().Count());
        }
    }
}