using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Containers.Base;

namespace HsLib.Types.Containers
{
    public class Field : MultiContainer<Minion>
    {
        public Field(Battlefield bf, Pid pid) : base(bf, new Place(pid, Loc.Field), limit: 7)
        {
        }

        public bool HasAnyActiveTaunt()
        {
            return CardTs.Any(c => c.Taunt.Value && !c.Stealth.Value);
        }

        protected override bool IsCardActive(ICard card)
        {
            return !((Minion)card).Dead;
        }
    }
}
