using Models.Cards;
using Models.Common;
using Models.Containers.Base;

namespace Models.Containers
{
    public class Hand : MultiContainer<Card>
    {
        public Hand(Pid pid) : base(pid, Loc.Hand)
        {
        }
    }
}
