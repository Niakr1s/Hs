using Models.Cards;
using Models.Common.Place;
using Models.Containers.Base;

namespace Models.Containers
{
    public class Deck : MultiContainer<Card>
    {
        public Deck(Battlefield bf, Pid pid, IEnumerable<Card>? cards = null) : base(bf, pid, Loc.Deck, startCards: cards)
        {
        }
    }
}
