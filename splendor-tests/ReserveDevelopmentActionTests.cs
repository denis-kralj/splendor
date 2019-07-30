using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using splendor_lib;

namespace splendor_tests
{
    public class ReserveDevelopmentActionTests
    {
        private List<Development> _developments;

        [OneTimeSetUp]
        public void SetUp()
        {
            const string pathToDevelopments = @"csv-data\developments-data.csv";
            GameDataLoader gdl = new GameDataLoader(pathToDevelopments, string.Empty);

            _developments = gdl.LoadDevelopments();
        }

        [Test]
        public void ShouldReserveDevelopmentFromPublicArea()
        {
            var reservee = new Player("Goku");
            
            var players = new List<Player>()
            {
                reservee,
                new Player("Vegeta"),
                new Player("Trunks")
            };

            var  board = new GameBoard((PlayerCount)players.Count, new List<Noble>(), _developments);

            var toReserve = board.PublicDevelopments.First();

            var sut = new ReserveDevelopmentAction(Location.Public, toReserve);

            Assert.IsTrue(sut.TryExecuteAction(reservee, board, out var result));

            Assert.AreEqual(ExecutionResult.Success, result);
        }
        
        [Test]
        public void ShouldNotReserveWhenHandFull()
        {
            var reservee = new Player("Goku");
            
            reservee.TryReserve(new Development(1,1,Token.Red, new TokenCollection()));
            reservee.TryReserve(new Development(1,1,Token.Red, new TokenCollection()));
            reservee.TryReserve(new Development(1,1,Token.Red, new TokenCollection()));
            
            var players = new List<Player>()
            {
                reservee,
                new Player("Vegeta"),
                new Player("Trunks")
            };

            var  board = new GameBoard((PlayerCount)players.Count, new List<Noble>(), _developments);

            var toReserve = board.PublicDevelopments.First();

            var sut = new ReserveDevelopmentAction(Location.Public, toReserve);

            Assert.IsFalse(sut.TryExecuteAction(reservee, board, out var result));

            Assert.AreEqual(ExecutionResult.HandFull, result);
        }
        
        [Test]
        public void ShouldNotReserveIfNotInPublicSpace()
        {
            var reservee = new Player("Goku");
            var fakeDevelopment = new Development(1,1,Token.Red, new TokenCollection());

            var players = new List<Player>()
            {
                reservee,
                new Player("Vegeta"),
                new Player("Trunks")
            };

            var board = new GameBoard((PlayerCount)players.Count, new List<Noble>(), _developments);

            var sut = new ReserveDevelopmentAction(Location.Public, fakeDevelopment);

            Assert.IsFalse(sut.TryExecuteAction(reservee, board, out var result));

            Assert.AreEqual(ExecutionResult.InvalidDevelopmentToReserve, result);
        }

        [Test]
        public void ShouldReserveFromDeck()
        {
            var reservee = new Player("Goku");

            var players = new List<Player>()
            {
                reservee,
                new Player("Vegeta"),
                new Player("Trunks")
            };

            var board = new GameBoard((PlayerCount)players.Count, new List<Noble>(), _developments);

            var sut = new ReserveDevelopmentAction(Location.Level2Deck, null);

            Assert.IsTrue(sut.TryExecuteAction(reservee, board, out var result));

            Assert.AreEqual(ExecutionResult.Success, result);

            Assert.AreEqual(1, reservee.ReservedDevelopments.Count);

            Assert.AreEqual(2, reservee.ReservedDevelopments.First().Level);
        }
    }
}