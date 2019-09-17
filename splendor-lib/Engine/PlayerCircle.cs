using System.Collections.Generic;
using System.Linq;

namespace splendor_lib
{
    public class PlayerCircle : IPlayerCircle
    {
        private int _indexInternal;

        private List<Player> _playersInternal;

        public Player Current => _playersInternal[_indexInternal];
        public bool LastPlayersTurn => Current == _playersInternal.Last();
        public void Pass()
        {
            if (++_indexInternal == _playersInternal.Count)
                _indexInternal = 0;
        }
        public PlayerCircle(string[] playerNames)
        {
            _indexInternal = 0;
            _playersInternal = new List<Player>(playerNames.Length);
            foreach (var name in playerNames)
            {
                _playersInternal.Add(new Player(name));
            }
        }
    }
}