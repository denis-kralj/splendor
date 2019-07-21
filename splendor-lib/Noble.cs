namespace splendor_lib
{
    public class Noble
    {
        public Noble(uint prestige, NobleCost cost)
        {
            Requirements = cost;
            this.Prestige = prestige;
        }
        public uint Prestige { get; }
        public NobleCost Requirements { get; private set; }
    }
}