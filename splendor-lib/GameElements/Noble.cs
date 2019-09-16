using System.Linq;

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
        public bool CanVisit(Player player)
        {
            if(TokenUtils.AllTokens.Any(t => player.Discount(t) < Requirements.Cost(t)))
                return false;

            return true;
        }
        public bool TryVisit(Player player)
        {
            if (!CanVisit(player))
                return false;

            player.TakeNoble(this);

            return true;
        }
    }
}