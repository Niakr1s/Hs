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

        public int ChooseCard(IEnumerable<Card> cards)
        {
            return Bf.Player.Player.ChooseCard(cards);
        }
    }
}