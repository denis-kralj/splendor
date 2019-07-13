namespace splendor_lib
{
    public class Noble
    {
        public Noble(int prestige, int diamondDevelopmentPrice, int rubyDevelopmentPrice, int emeraldDevelopmentPrice, int onyxDevelopmentPrice, int sapphireDevelopmentPricet)
        {
            this.OnyxDevelopmentPrice = onyxDevelopmentPrice;
            this.EmeraldDevelopmentPrice = emeraldDevelopmentPrice;
            this.RubyDevelopmentPrice = rubyDevelopmentPrice;
            this.DiamondDevelopmentPrice = diamondDevelopmentPrice;
            this.SapphireDevelopmentPricet = sapphireDevelopmentPricet;
            this.Prestige = prestige;
        }

        public int Prestige { get; }
        public int SapphireDevelopmentPricet { get; }
        public int DiamondDevelopmentPrice { get; }
        public int RubyDevelopmentPrice { get; }
        public int EmeraldDevelopmentPrice { get; }
        public int OnyxDevelopmentPrice { get; }
    }
}