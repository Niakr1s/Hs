using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Containers.Base;

namespace HsLib.Types.Containers
{
    public class Deck : MultiContainer<IPlayableFromHand>
    {
        public Deck(Battlefield bf, Pid pid, IEnumerable<Card>? cards = null) : base(bf, new Place(pid, Loc.Deck), startCards: cards)
        {
        }
    }
}
