using System;
using System.Collections.Generic;
using System.Linq;

namespace splendor_lib;

public class Deck<TCard>
{
    private List<TCard> _allCardsInternal;
    private Stack<TCard> _stackInternal;
    public bool IsEmpty => Count == 0;
    public int Count => _stackInternal.Count;
    public Deck(List<TCard> cards)
    {
        this._allCardsInternal = cards;
        this._stackInternal = new Stack<TCard>(_allCardsInternal);
        Shuffle();
    }
    public void Shuffle(bool isReshuffle = false)
    {
        if (isReshuffle)
            this._stackInternal = new Stack<TCard>(_allCardsInternal);

        var seed = new Random();
        _stackInternal = _stackInternal
          .Select(c => new { Index = seed.Next(), Card = c })
          .OrderBy(c => c.Index)
          .Select(c => c.Card)
          .ToStack();
    }
    public List<TCard> Draw(uint count = 1)
    {
        if (Count < count)
            count = (uint)_stackInternal.Count;

        return _stackInternal.Pop<TCard>(count);
    }
}
