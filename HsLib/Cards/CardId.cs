namespace HsLib.Cards
{
    public enum CardId
    {
        // minions
        AbusiveSergeant,
        ChillwindYeti,

        // heros and abilities
        JainaProudmoore, Fireblast,
    }

    public static class CardIdExtensions
    {
        public static Card ToCard(this CardId cardId)
        {
            return CardBuilder.FromId(cardId);
        }
    }
}
