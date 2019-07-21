using System;
using System.Collections.Generic;
using System.Linq;

namespace splendor_lib
{
    public class Deck<TCard>
    {
        private List<TCard> _allCardsInternal;
        private Stack<TCard> _stackInternal;
        public Deck(List<TCard> cards)
        {
            this._allCardsInternal = cards;
            this._stackInternal = new Stack<TCard>(cards);
        }
        public Deck(TCard[] cards) : this(new List<TCard>(cards)) { }
        public bool IsEmpty => _stackInternal.Count == 0;
        public int Count => _stackInternal.Count;
        public void ShuffleAll() => _stackInternal = ShuffleDeck();
        public Stack<TCard> ShuffleDeck()
        {
            var seed = new Random();
            return
                _stackInternal
                .Select(c => new { Index = seed.Next(), Card = c})
                .OrderBy(c => c.Index)
                .Select(c => c.Card)
                .ToStack();
        }
        public bool TryDraw(out List<TCard> drawn, bool drawTillEnd = false, uint count = 1)
        {
            drawn = null;

            if(_stackInternal.Count == 0 || count == 0)
                return false;

            if(_stackInternal.Count < count && !drawTillEnd)
                return false;

            if(_stackInternal.Count < count && drawTillEnd)
                count = (uint)_stackInternal.Count;

            drawn = _stackInternal.Pop<TCard>(count);

            return true;
        }
    }
}