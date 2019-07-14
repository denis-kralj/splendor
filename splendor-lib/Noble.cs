using System.Collections.Generic;

namespace splendor_lib
{
    public class Noble
    {
        public Noble(int prestige, int dDevRequirement, int rDevRequirement, int eDevRequirement, int oDevRequirement, int sDevRequirement)
        {
            Requirements = new Dictionary<Token, int>
            {
                { Token.White, dDevRequirement },
                { Token.Black, oDevRequirement },
                { Token.Blue,  sDevRequirement },
                { Token.Green, eDevRequirement },
                { Token.Red,   rDevRequirement }
            };

            this.Prestige = prestige;
        }

        public int Prestige { get; }
        public Dictionary<Token, int> Requirements { get; private set; }
    }
}