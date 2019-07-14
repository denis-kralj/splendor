namespace splendor_lib
{
    public class Noble
    {
        public Noble(int prestige, int diamondDevelopmentPrice, int rubyDevelopmentPrice, int emeraldDevelopmentPrice, int onyxDevelopmentPrice, int sapphireDevelopmentPrice)
        {
            this.OnyxDevelopmentPrice = onyxDevelopmentPrice;
            this.EmeraldDevelopmentPrice = emeraldDevelopmentPrice;
            this.RubyDevelopmentPrice = rubyDevelopmentPrice;
            this.DiamondDevelopmentPrice = diamondDevelopmentPrice;
            this.SapphireDevelopmentPrice = sapphireDevelopmentPrice;
            this.Prestige = prestige;
        }

        public int Prestige { get; }
        public int SapphireDevelopmentPrice { get; }
        public int DiamondDevelopmentPrice { get; }
        public int RubyDevelopmentPrice { get; }
        public int EmeraldDevelopmentPrice { get; }
        public int OnyxDevelopmentPrice { get; }
    }
}