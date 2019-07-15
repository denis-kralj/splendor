using System;
using System.Collections.Generic;
using System.Linq;

namespace splendor_lib
{
    public class TokenCollection
    {
        private Dictionary<Token, int> _tokens;

        public int TotalTokens => _tokens.Values.Sum(v => v);

        public int this[Token index] => _tokens[index];
        
        public TokenCollection()
        {
            InitializeCollection();
        }

        private void InitializeCollection()
        {
            _tokens = new Dictionary<Token, int>
            {
                { Token.White,  0 },
                { Token.Black,  0 },
                { Token.Blue,   0 },
                { Token.Green,  0 },
                { Token.Red,    0 },
                { Token.Yellow, 0 }
            };
        }

        public void AddTokens(params Token[] tokens)
        {
            foreach(var token in tokens)
                _tokens[token]++;
        }

        public void RemoveTokens(params Token[] tokens)
        {
            foreach(var token in tokens)
            {
                if(_tokens[token] == 0)
                    throw new InsufficientTokensException("Not enough tokens to remove");
                
                _tokens[token]--;
            }
        }
    }
}