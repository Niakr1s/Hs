using HsLib.Cards.Abilities;
using HsLib.Cards.Heroes;
using HsLib.Cards.Minions;


namespace HsLib.Types.Cards
{
    public static class CardBuilder
    {
        /// <summary>
        /// Builds new card from id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>new card</returns>
        /// <exception cref="NotSupportedException"></exception>
        public static Card FromId(CardId id)
        {

            return id switch
            {
                CardId.AbusiveSergeant => new AbusiveSergeant(),
                CardId.ChillwindYeti => new ChillwindYeti(),
                CardId.FlametongTotem => new FlametongTotem(),
                CardId.Abomination => new Abomintaion(),

                CardId.JainaProudmoore => new JainaProudmoore(),
                CardId.Fireblast => new Fireblast(),
                CardId.GarroshHellscream => new GarroshHellscream(),
                CardId.ArmorUp => new ArmorUp(),

                _ => throw new NotSupportedException()
            };
        }
    }
}
