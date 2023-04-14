using Models.Cards;
using Models.Common;
using Models.Containers.Base;

namespace Models.Containers
{
    public class Deck : MultiContainer<Card>
    {
        public Deck(Pid pid, IEnumerable<Card>? cards = null) : base(pid, Loc.Deck, startCards: cards)
        {
        }
    }
}
