using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace splendor_lib
{
    public class Development
    {
        public Development(int level, int prestige, Token discounts, int dPrice, int rPrice, int ePrice, int oPrice, int sPrice)
        {
            Level = level;
            Prestige = prestige;
            Cost = new Dictionary<Token, int>
            {
                { Token.White, dPrice },
                { Token.Black, oPrice },
                { Token.Blue,  sPrice },
                { Token.Green, ePrice },
                { Token.Red,   rPrice }
            }.AsReadOnly();
            Discounts = discounts;
        }
        public int Level { get; private set; }
        public int Prestige { get; private set; }
        public Token Discounts { get; private set; }
        public ReadOnlyDictionary<Token,int> Cost {get;private set; }

        public static bool operator ==(Development obj1, Development obj2)
        {
            return obj1 as object != null && obj1.Equals(obj2);
        }

        public static bool operator !=(Development obj1, Development obj2)
        {
            return !(obj1 == obj2);
        }

        public override bool Equals(object obj)
        {
            var castObj = obj as Development;
            
            return
                (obj != null) &&
                castObj.Level == this.Level &&
                castObj.Prestige == this.Prestige &&
                castObj.Discounts == this.Discounts &&
                castObj.Cost[Token.White] == this.Cost[Token.White] &&
                castObj.Cost[Token.Red] == this.Cost[Token.Red] &&
                castObj.Cost[Token.Green] == this.Cost[Token.Green] &&
                castObj.Cost[Token.Black] == this.Cost[Token.Black] &&
                castObj.Cost[Token.Blue] == this.Cost[Token.Blue];
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}