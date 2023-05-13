using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Containers.Base;

namespace HsLib.Types.Containers
{
    public class Deck : MultiContainer<Card>
    {
        public Deck(Battlefield bf, Pid pid, IEnumerable<Card>? cards = null) : base(bf, pid, Loc.Deck, startCards: cards)
        {
        }

        public override IEnumerable<Card> CleanInactiveCards()
        {
            yield break;
        }
    }
}
