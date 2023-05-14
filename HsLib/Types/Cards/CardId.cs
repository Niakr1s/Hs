namespace HsLib.Types.Cards
{
    public enum CardId
    {
        // minions
        AbusiveSergeant,
        ChillwindYeti,
        FlametongTotem,
        Abomination,

        DruidOfTheClaw, DruidOfTheClawTaunt, DruidOfTheClawCharge,

        // heros and abilities
        JainaProudmoore, Fireblast,
        GarroshHellscream, ArmorUp,
    }

    public static class CardIdExtensions
    {
        public static Card ToCard(this CardId cardId)
        {
            return CardBuilder.FromId(cardId);
        }
    }
}
