using System;
using System.Collections.Generic;
using System.Linq;

namespace splendor_lib;

public class Deck<TCard> : IDeck<TCard>
{
    private List<TCard> _allCardsInternal;
    private Stack<TCard> _stackInternal;

    public Deck(List<TCard> cards)
    {
        this._allCardsInternal = cards;
        this._stackInternal = new Stack<TCard>(_allCardsInternal);
        Shuffle();
    }

    public bool IsEmpty => Count == 0;
    public int Count => _stackInternal.Count;

    public void Shuffle(bool shuffleEverythingBack = false)
    {
        if (shuffleEverythingBack)
            this._stackInternal = new Stack<TCard>(_allCardsInternal);

        var seed = new Random();
        _stackInternal = new Stack<TCard>(
            _stackInternal
            .Select(c => new { Index = seed.Next(), Card = c })
            .OrderBy(c => c.Index)
            .Select(c => c.Card)
        );
    }

    public List<TCard> Draw(uint count = 1)
    {
        if (Count < count)
            count = (uint)_stackInternal.Count;

        return _stackInternal.Pop<TCard>(count);
    }
}

internal static class DeckExtensions
{
    public static List<T> Pop<T>(this Stack<T> stack, uint amount)
    {
        var result = new List<T>((int)amount);

        for (int i = 0; i < amount; i++)
            result.Add(stack.Pop());

        return result;
    }
}