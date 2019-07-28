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
        public List<Noble> BoardNobles => new List<Noble>(_publicNoblesInternal);
        public IReadOnlyTokenCollection BoardTokens => _boardTokensInternal;
        public void RecieveTokens(TokenCollection tokensToReturnToBoard) => _boardTokensInternal.AddTokens(tokensToReturnToBoard);
        public GameBoard(PlayerCount playerCount, List<Noble> nobles, List<Development> developments) => SetupBoard(playerCount, nobles, developments);
        public bool TryTakeTokens(TokenCollection tokensToGetFromBoard) => _boardTokensInternal.TryTake(tokensToGetFromBoard);
        public bool TryTakeTokens(Token tokenType, uint count) => _boardTokensInternal.TryTake(tokenType, count);
        public bool TryRemoveDevelopment(Location location, Development developmentToTake, out Development actuallyTaken)
        {
            switch (location)
            {
                case Location.Public: default: return TakeFromPublic(developmentToTake, out actuallyTaken);
                case Location.Level1Deck: return TakeFromDeck(_lvl1Deck, out actuallyTaken);
                case Location.Level2Deck: return TakeFromDeck(_lvl2Deck, out actuallyTaken);
                case Location.Level3Deck: return TakeFromDeck(_lvl3Deck, out actuallyTaken);
            }
        }
        private bool TakeFromDeck(Deck<Development> lvl1Deck, out Development actuallyTaken)
        {
            List<Development> drawn;

            if(lvl1Deck.TryDraw(out drawn))
            {
                actuallyTaken = drawn.First();
                return true;
            }

            actuallyTaken = null;
            return false;
        }
        private bool TakeFromPublic(Development developmentToTake, out Development actuallyTaken)
        {
            if(PublicDevelopments.Contains(developmentToTake))
            {
                actuallyTaken = developmentToTake;
                _boardDevelopmentsInternal.Remove(developmentToTake);
                return true;
            }

            actuallyTaken = null;
            return false;
        }
        public void SetupBoard(PlayerCount playerCount, List<Noble> nobles, List<Development> developments)
        {
            LoadDecks(nobles, developments);
            ShuffleAllDecks();
            DrawInitialBoardDevelopments();
            DrawNobles((int)playerCount);
            InitTokens(playerCount);
        }
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
        private void DrawNobles(int playerCount) => _noblesDeck.TryDraw(out _publicNoblesInternal, false, (uint)playerCount + 1);
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

        public List<Development> PublicDevelopments => _boardDevelopmentsInternal;
    }
}