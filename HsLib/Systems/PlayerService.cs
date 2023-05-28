using HsLib.Types.Cards;

namespace HsLib.Systems
{
    public class PlayerService
    {
        public PlayerService(Board board)
        {
            Board = board;
        }

        public Board Board { get; }

        public CardId ChooseCard(IEnumerable<CardId> cards)
        {
            return Board.Player.Player.ChooseOne(cards);
        }
    }
}