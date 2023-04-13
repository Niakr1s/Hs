using Models.Cards;
using Models.Common;
using Models.Containers.Base;

namespace Models.Containers
{
    public class Hand : MultiContainer<Card>
    {
        public Hand(Battlefield bf, Pid pid) : base(bf, pid, Loc.Hand)
        {
        }
    }
}
