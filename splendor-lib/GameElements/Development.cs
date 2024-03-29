namespace splendor_lib;

public class Development
{
    public Development(uint level, uint prestige, Token discounts, IReadOnlyTokenCollection cost)
    {
        Level = level;
        Prestige = prestige;
        Cost = cost;
        Discounts = discounts;
    }

    public uint Level { get; }
    public uint Prestige { get; }
    public Token Discounts { get; }
    public IReadOnlyTokenCollection Cost { get; }

    public static bool operator ==(Development obj1, Development obj2) => obj1 as object != null && obj1.Equals(obj2);

    public static bool operator !=(Development obj1, Development obj2) => !(obj1 == obj2);

    public override int GetHashCode() => base.GetHashCode();

    public override bool Equals(object obj) =>
        (obj != null) && (obj as Development != null) &&
        (obj as Development).Level == this.Level &&
        (obj as Development).Prestige == this.Prestige &&
        (obj as Development).Discounts == this.Discounts &&
        (obj as Development).Cost == this.Cost;
}
