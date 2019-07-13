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
            var pathToDevelopments = @"csv-data\developments-data.csv";
            var pathToNobles = @"..\csv-data\nobles-data.csv";

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
            Assert.AreEqual(allDevelopmentCards.Count, allDevelopmentCards.Distinct().Count());
        }

        public void LoadAllNobleCards()
        {
            var allNobles = _sut.LoadNobles();
            Assert.AreEqual(10, allNobles.Count);
        }

        public void AllNobleCardsAreDistinct()
        {
            var allNobles = _sut.LoadNobles();
            Assert.AreEqual(allNobles.Count, allNobles.Distinct().Count());
        }
    }
}