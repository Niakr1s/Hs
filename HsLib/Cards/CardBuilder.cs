using HsLib.Cards.KnownCards.Minions;

namespace HsLib.Cards
{
    public class CardBuilder
    {
        public Card? FromId(CardId id)
        {

            return id switch
            {
                CardId.AbusiveSergeant => new AbusiveSergeant(),
                CardId.ChillwindYeti => new ChillwindYeti(),

                _ => null,
            };
        }
    }
}
