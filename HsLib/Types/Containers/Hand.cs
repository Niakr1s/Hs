using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types.Containers.Base;

namespace HsLib.Types.Containers
{
    public class Hand : MultiContainer<IPlayableFromHand>
    {
        public Hand(Battlefield bf, Pid pid) : base(bf, new Place(pid, Loc.Hand), limit: 10)
        {
        }
    }
}
