namespace splendor_lib
{
    public class NobleCost
    {
        private TokenCollection _costInternal;
        public NobleCost() : this(new TokenCollection()) { }
        public NobleCost(TokenCollection cost) => _costInternal = cost;
        public uint Cost(TokenColor tokenColor) => _costInternal.GetCount(tokenColor);
    }
}