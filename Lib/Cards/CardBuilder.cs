using Models.Cards.KnownCards.Minions;

namespace Models.Cards
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
