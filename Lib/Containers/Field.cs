using Models.Cards;
using Models.Common;
using Models.Containers.Container;

namespace Models.Containers
{
    public class Field : MultiContainer<Minion>
    {
        public Field(Battlefield bf, Pid pid) : base(bf, pid, Loc.Field, limit: 7)
        {
        }
    }
}
