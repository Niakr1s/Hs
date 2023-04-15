using HsLib.Battle;
using HsLib.Cards;
using HsLib.Common.Place;
using HsLib.Containers.Base;

namespace HsLib.Containers
{
    public class Field : MultiContainer<Minion>
    {
        public Field(Battlefield bf, Pid pid) : base(bf, pid, Loc.Field, limit: 7)
        {
        }
    }
}
