using HsLib.Cards.KnownCards.Abilities;
using HsLib.Cards.KnownCards.Heroes;
using HsLib.Cards.KnownCards.Minions;

namespace HsLib.Cards
{
    public static class CardBuilder
    {
        public static Card FromId(CardId id)
        {

            return id switch
            {
                CardId.AbusiveSergeant => new AbusiveSergeant(),
                CardId.ChillwindYeti => new ChillwindYeti(),

                CardId.JainaProudmoore => new JainaProudmoore(),
                CardId.Fireblast => new Fireblast(),

                _ => throw new NotSupportedException()
            };
        }
    }
}
