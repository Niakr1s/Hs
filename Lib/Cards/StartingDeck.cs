using Models.Cards;

namespace Models.Common
{
    public class StartingDeck
    {
        public StartingDeck(Card hero) : this(hero, new List<Card>())
        {

        }

        public StartingDeck(Card hero, IEnumerable<Card> cards)
        {
            Hero = hero;
            Cards = cards.ToList();
        }

        public Card Hero { get; }
        public List<Card> Cards { get; }
    }
}
