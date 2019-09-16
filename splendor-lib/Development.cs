namespace splendor_lib
{
    public class Development
    {
        private IReadOnlyTokenCollection _costInternal;
        public Development(uint level, uint prestige, TokenColor discounts, IReadOnlyTokenCollection cost)
        {
            Level = level;
            Prestige = prestige;
            _costInternal = cost;
            Discounts = discounts;
        }
        public uint Level { get; private set; }
        public uint Prestige { get; private set; }
        public TokenColor Discounts { get; private set; }
        public IReadOnlyTokenCollection Cost => _costInternal;
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
}