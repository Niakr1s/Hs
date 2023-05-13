using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Containers.Base;

namespace HsLib.Types.Containers
{
    public class Graveyard : MultiContainer<Card>
    {
        public Graveyard(Battlefield bf, Pid pid) : base(bf, pid, Loc.Graveyard)
        {
        }

        public override IEnumerable<Card> CleanInactiveCards()
        {
            yield break;
        }
    }
}
