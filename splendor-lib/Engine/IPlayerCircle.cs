namespace splendor_lib
{
    public interface IPlayerCircle
    {
        Player Current { get; }
        bool LastPlayersTurn { get; }
        void Pass();
    }
}