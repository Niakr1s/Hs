using Models.Cards;
using Models.Common;
using Models.Containers.Container;

namespace Models.Containers
{
    public class Deck : MultiContainer<Card>
    {
        public Deck(Battlefield bf, Pid pid, IEnumerable<Card>? cards = null) :
            base(bf, pid, Loc.Deck, startCards: cards)
        {
        }
    }
}
