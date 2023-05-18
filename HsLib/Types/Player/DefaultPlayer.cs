using HsLib.Types.Cards;

namespace HsLib.Types.Player
{
    public class DefaultPlayer : IPlayer
    {
        public CardId ChooseOne(IEnumerable<CardId> cards)
        {
            return cards.First();
        }
    }
}
