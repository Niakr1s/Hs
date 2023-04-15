using HsLib.Cards;
using HsLib.Common.Place;
using HsLib.Containers.Base;

namespace HsLib.Containers
{
    public class Hand : MultiContainer<Card>
    {
        public Hand(Battlefield bf, Pid pid) : base(bf, pid, Loc.Hand)
        {
        }
    }
}
