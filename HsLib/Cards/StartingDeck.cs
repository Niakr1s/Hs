using HsLib.Containers;

namespace HsLib.Cards
{
    public class StartingDeck
    {
        public StartingDeck(HeroId heroId) : this(heroId, new List<Card>())
        {

        }

        public StartingDeck(HeroId heroId, IEnumerable<Card> cards)
        {
            HeroId = heroId;
            Cards = cards.ToList();
        }

        public HeroId HeroId { get; }
        public List<Card> Cards { get; }
    }
}
