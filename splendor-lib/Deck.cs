using System;
using System.Collections.Generic;
using System.Linq;

namespace splendor_lib
{
    public class Deck<TCard>
    {
        private List<TCard> _allCards;
        private Stack<TCard> _deckStack;

        public bool IsEmpty => _deckStack.Count == 0;

        public int Count => _deckStack.Count;
        public Deck(List<TCard> cards)
        {
            this._allCards = cards;
            this._deckStack = new Stack<TCard>(cards);
        }

        public Deck(TCard[] cards) : this(new List<TCard>(cards))
        {
        }

        public void ShuffleAll()
        {
            _deckStack = new Stack<TCard>(_allCards);
            ShuffleDeck();
        }

        public void ShuffleDeck()
        {
            var seed = new Random();
            _deckStack =
                _deckStack
                .Select(c => new { Index = seed.Next(), Card = c})
                .OrderBy(c => c.Index)
                .Select(c => c.Card)
                .ToStack();
        }



        public IEnumerable<TCard> Draw(int count = 1)
        {
            if(count < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(count), count, "count cannot be negative");

            if(_deckStack.Count == 0)
                throw new DeckException("Can't draw from an empty deck");

            if(_deckStack.Count < count)
                count = _deckStack.Count;

            var result = new List<TCard>(count);

            while(count-- != 0) result.Add(_deckStack.Pop());

            return result;
        }
    }
}