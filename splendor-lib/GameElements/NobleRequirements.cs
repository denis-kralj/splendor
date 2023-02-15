namespace splendor_lib;

public class NobleRequirements
{
    private TokenCollection _requirementsInternal;

    public NobleRequirements(TokenCollection cost) => _requirementsInternal = cost;

    public uint Cost(Token type) => _requirementsInternal.GetCount(type);
}
