namespace HsLib.Types.Cards
{
    public class StartingDeck
    {
        public StartingDeck(CardId heroId) : this(heroId, new List<IPlayableFromHand>())
        {

        }

        public StartingDeck(CardId heroId, IEnumerable<IPlayableFromHand> cards)
        {
            HeroId = heroId;
            Cards = cards.ToList();
        }

        public CardId HeroId { get; }

        public IEnumerable<IPlayableFromHand> Cards { get; }
    }
}
