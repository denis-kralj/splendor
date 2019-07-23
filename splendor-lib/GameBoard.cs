using System;
using System.Collections.Generic;
using System.Linq;

namespace splendor_lib
{
    public class GameBoard
    {
        private Deck<Development> _lvl1Deck;
        private Deck<Development> _lvl2Deck;
        private Deck<Development> _lvl3Deck;
        private Deck<Noble> _noblesDeck;
        private List<Development> _boardDevelopmentsInternal;
        private List<Noble> _publicNoblesInternal;
        private TokenCollection _boardTokensInternal;
        public GameBoard(PlayerCount playerCount, List<Noble> nobles, List<Development> developments)
        {
            SetupBoard(playerCount, nobles, developments);
        }
        public void SetupBoard(PlayerCount playerCount, List<Noble> nobles, List<Development> developments)
        {
            LoadDecks(nobles, developments);
            ShuffleAllDecks();
            DrawInitialBoardDevelopments();
            DrawNobles((int)playerCount);
            InitTokens(playerCount);
        }

        public List<Noble> BoardNobles => new List<Noble>(_publicNoblesInternal);

        public TokenCollection BoardTokens => new TokenCollection(_boardTokensInternal);

        private void InitTokens(PlayerCount playerCount)
        {
            switch (playerCount)
            {
                case PlayerCount.Four:
                    _boardTokensInternal = new TokenCollection(7, 7, 7, 7, 7, 5);
                    break;
                case PlayerCount.Three:
                    _boardTokensInternal = new TokenCollection(5, 5, 5, 5, 5, 5);
                    break;
                case PlayerCount.Two: default:
                    _boardTokensInternal = new TokenCollection(4, 4, 4, 4, 4, 5);
                    break;
            }
        }
        private void LoadDecks(List<Noble> nobles, List<Development> developments)
        {
            _lvl1Deck = new Deck<Development>(developments.Where(d => d.Level == 1).ToList());
            _lvl2Deck = new Deck<Development>(developments.Where(d => d.Level == 2).ToList());
            _lvl3Deck = new Deck<Development>(developments.Where(d => d.Level == 3).ToList());
            _noblesDeck = new Deck<Noble>(nobles);
        }
        private void ShuffleAllDecks()
        {
            _lvl1Deck.ShuffleAll();
            _lvl2Deck.ShuffleAll();
            _lvl3Deck.ShuffleAll();
            _noblesDeck.ShuffleAll();
        }

        public void RecieveTokens(TokenCollection tokensToReturnToBoard)
        {
            _boardTokensInternal.AddTokens(tokensToReturnToBoard);
        }

        private void DrawNobles(int playerCount)
        {
            _noblesDeck.TryDraw(out _publicNoblesInternal, false, (uint)playerCount + 1);
        }
        private void DrawInitialBoardDevelopments()
        {
            uint drawPerDeck = 4;
            int boardCap = (int)drawPerDeck * 3;

            _boardDevelopmentsInternal = new List<Development>(boardCap);

            List<Development> draw;

            if (_lvl1Deck.TryDraw(out draw, false, drawPerDeck))
                _boardDevelopmentsInternal.AddRange(draw);

            if (_lvl2Deck.TryDraw(out draw, false, drawPerDeck))
                _boardDevelopmentsInternal.AddRange(draw);

            if (_lvl3Deck.TryDraw(out draw, false, drawPerDeck))
                _boardDevelopmentsInternal.AddRange(draw);
        }
    }
}