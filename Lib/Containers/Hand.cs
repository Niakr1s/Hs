using Models.Cards;
using Models.Common;
using Models.Containers.Container;

namespace Models.Containers
{
    public class Hand : MultiContainer<Card>
    {
        public Hand(Battlefield bf, Pid pid) : base(bf, pid, Loc.Hand)
        {
        }
    }
}
