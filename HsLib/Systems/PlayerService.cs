using HsLib.Types.Cards;

namespace HsLib.Systems
{
    public class PlayerService : Service
    {
        public PlayerService(Board board) : base(board)
        {
        }

        public CardId ChooseCard(IEnumerable<CardId> cards)
        {
            return Board.Player.Player.ChooseOne(cards);
        }
    }
}