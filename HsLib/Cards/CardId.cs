namespace HsLib.Cards
{
    public enum CardId
    {
        // minions
        AbusiveSergeant,
        ChillwindYeti,

        // heros
        JainaProudmoore,
    }

    public static class CardIdExtensions
    {
        public static Card ToCard(this CardId cardId)
        {
            return CardBuilder.FromId(cardId);
        }
    }
}
