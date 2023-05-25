using HsLib.KnownCards.Abilities;
using HsLib.KnownCards.Heroes;
using HsLib.KnownCards.Minions;


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
                CardId.DruidOfTheClaw => new DruidOfTheClaw(),
                CardId.DruidOfTheClawCharge => new DruidOfTheClawCharge(),
                CardId.DruidOfTheClawTaunt => new DruidOfTheClawTaunt(),
                CardId.Armorsmith => new Armorsmith(),
                CardId.Doomsayer => new Doomsayer(),
                CardId.FacelessManipulator => new FacelessManipulator(),

                CardId.JainaProudmoore => new JainaProudmoore(),
                CardId.Fireblast => new Fireblast(),
                CardId.GarroshHellscream => new GarroshHellscream(),
                CardId.ArmorUp => new ArmorUp(),

                _ => throw new NotSupportedException()
            };
        }
    }
}
