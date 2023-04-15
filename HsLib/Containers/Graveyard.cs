using HsLib.Cards;
using HsLib.Common.Place;
using HsLib.Containers.Base;

namespace HsLib.Containers
{
    public class Graveyard : MultiContainer<Card>
    {
        public Graveyard(Battlefield bf, Pid pid) : base(bf, pid, Loc.Graveyard)
        {
        }
    }
}
