using Models.Cards;
using Models.Common.Place;
using Models.Containers.Base;

namespace Models.Containers
{
    public class Field : MultiContainer<Minion>
    {
        public Field(Pid pid) : base(pid, Loc.Field, limit: 7)
        {
        }
    }
}
