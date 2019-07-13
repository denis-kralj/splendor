namespace splendor_lib
{
    public class Development
    {
        public Development(int level, int prestige, Color discounts, int diamondPrice, int rubyPrice, int emeraldPrice, int onyxPrice, int sapphirePrice)
        {
            Level = level;
            Prestige = prestige;
            Discounts = discounts;
            DiamondPrice = diamondPrice;
            RubyPrice = rubyPrice;
            EmeraldPrice = emeraldPrice;
            OnyxPrice = onyxPrice;
            SapphirePrice = sapphirePrice;
        }
        public int Level { get; private set; }
        public int Prestige { get; private set; }
        public Color Discounts { get; private set; }
        public int DiamondPrice { get; private set; }
        public int RubyPrice { get; private set; }
        public int EmeraldPrice { get; private set; }
        public int OnyxPrice { get; private set; }
        public int SapphirePrice { get; private set; }

        public static bool operator ==(Development obj1, Development obj2)
        {
            return obj1.Equals(obj2);
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
                castObj.DiamondPrice == this.DiamondPrice &&
                castObj.RubyPrice == this.RubyPrice &&
                castObj.EmeraldPrice == this.EmeraldPrice &&
                castObj.OnyxPrice == this.OnyxPrice &&
                castObj.SapphirePrice == this.SapphirePrice;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}