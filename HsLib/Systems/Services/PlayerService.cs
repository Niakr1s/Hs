using HsLib.Types.Cards;

namespace HsLib.Systems.Services
{
    public class PlayerService
    {
        public PlayerService(Battlefield bf)
        {
            Bf = bf;
        }

        public Battlefield Bf { get; }

        public CardId ChooseCard(IEnumerable<CardId> cards)
        {
            return Bf.Player.Player.ChooseOne(cards);
        }
    }
}