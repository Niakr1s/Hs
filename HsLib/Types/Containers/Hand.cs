using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Containers.Base;

namespace HsLib.Types.Containers
{
    public class Hand : MultiContainer<Card>
    {
        public Hand(Battlefield bf, Pid pid) : base(bf, pid, Loc.Hand)
        {
        }

        public override IEnumerable<Card> CleanInactiveCards()
        {
            yield break;
        }
    }
}
