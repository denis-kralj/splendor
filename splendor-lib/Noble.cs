using System;

namespace splendor_lib
{
    public class Noble
    {
        public Noble(uint prestige, NobleCost cost)
        {
            Requirements = cost;
            this.Prestige = prestige;
        }
        public Noble(uint prestige, TokenCollection cost) : this(prestige, new NobleCost(cost)) { }
        public uint Prestige { get; }
        public NobleCost Requirements { get; private set; }
        public bool TryVisit(Player player)
        {
            foreach(Token tokenType in Enum.GetValues(typeof(Token)))
                if(player.Discount(tokenType) < Requirements.Cost(tokenType))
                    return false;

            player.TakeNoble(this);

            return true;
        }
    }
}