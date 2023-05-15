using HsLib.Interfaces;
using HsLib.Types.Cards;

namespace HsLib.Types
{
    public class DefaultPlayer : IPlayer
    {
        public CardId ChooseOne(IEnumerable<CardId> cards)
        {
            return cards.First();
        }
    }
}
