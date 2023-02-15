using System.Collections.Generic;

namespace splendor_lib;

public interface IDeck<TCard>
{
    bool IsEmpty { get; }
    int Count { get; }
    List<TCard> Draw(uint count = 1);
    void Shuffle(bool shuffleEverythingBack = false);
}
