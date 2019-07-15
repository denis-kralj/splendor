using NUnit.Framework;
using splendor_lib;

namespace splendor_tests
{
    public class TokenCollectionTests
    {
        private TokenCollection _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new TokenCollection();
        }

        [Test]
        public void ShouldAddTokensToCollection()
        {
            _sut.AddTokens(Token.Black, Token.Black, Token.Blue);

            Assert.AreEqual(_sut.TotalTokens, 3);
        }

        [Test]
        public void ShouldRemoveTokens()
        {
            _sut.AddTokens(Token.Black, Token.Black, Token.Blue);

            Assert.AreEqual(_sut.TotalTokens, 3);

            _sut.RemoveTokens(Token.Black, Token.Blue);

            Assert.AreEqual(_sut.TotalTokens, 1);
        }

        [Test]
        public void ShouldThrowWhenInsufficientTokens()
        {
            Assert.Throws<InsufficientTokensException>(() => _sut.RemoveTokens(Token.Blue));
        }

        [Test]
        public void CanCountSpecificTokenTypeContained()
        {
            _sut.AddTokens(Token.Black, Token.Black, Token.Blue, Token.Red, Token.Red, Token.Yellow);

            Assert.AreEqual(_sut[Token.Black], 2);
            Assert.AreEqual(_sut[Token.Blue], 1);
            Assert.AreEqual(_sut[Token.Green], 0);
            Assert.AreEqual(_sut[Token.Red], 2);
            Assert.AreEqual(_sut[Token.White], 0);
            Assert.AreEqual(_sut[Token.Yellow], 1);
        }
    }
}