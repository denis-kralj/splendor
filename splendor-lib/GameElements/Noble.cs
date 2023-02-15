using System.Linq;

namespace splendor_lib;

public class Noble
{
    public Noble(uint prestige, NobleRequirements cost)
    {
        Requirements = cost;
        Prestige = prestige;
    }

    public uint Prestige { get; }
    public NobleRequirements Requirements { get; }

    public bool CanVisit(Player player)
    {
        if (Tokens.AllTokens.Any(t => player.Discount(t) < Requirements.Cost(t)))
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
