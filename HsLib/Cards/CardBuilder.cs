using HsLib.Cards.KnownCards.Heroes;
using HsLib.Cards.KnownCards.Minions;

namespace HsLib.Cards
{
    public class CardBuilder
    {
        public static Card FromId(CardId id)
        {

            return id switch
            {
                CardId.AbusiveSergeant => new AbusiveSergeant(),
                CardId.ChillwindYeti => new ChillwindYeti(),
                CardId.JainaProudmoore => new JainaProudmoore(),
                _ => throw new NotSupportedException()
            };
        }
    }
}
