using System.Collections.Generic;

namespace splendor_lib
{
    public class Noble
    {
        public Noble(uint prestige, uint dDevRequirement, uint rDevRequirement, uint eDevRequirement, uint oDevRequirement, uint sDevRequirement)
        {
            Requirements = new Dictionary<Token, uint>
            {
                { Token.White, dDevRequirement },
                { Token.Black, oDevRequirement },
                { Token.Blue,  sDevRequirement },
                { Token.Green, eDevRequirement },
                { Token.Red,   rDevRequirement }
            };

            this.Prestige = prestige;
        }

        public uint Prestige { get; }
        public Dictionary<Token, uint> Requirements { get; private set; }
    }
}