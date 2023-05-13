namespace HsLib.Types.Cards
{
    public class StartingDeck
    {
        public StartingDeck(CardId heroId) : this(heroId, new List<Card>())
        {

        }

        public StartingDeck(CardId heroId, IEnumerable<Card> cards)
        {
            HeroId = heroId;
            Cards = cards.ToList();
        }

        public CardId HeroId { get; }
        public List<Card> Cards { get; }
    }
}
