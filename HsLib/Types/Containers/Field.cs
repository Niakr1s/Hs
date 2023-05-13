using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Containers.Base;

namespace HsLib.Types.Containers
{
    public class Field : MultiContainer<Minion>
    {
        public Field(Battlefield bf, Pid pid) : base(bf, pid, Loc.Field, limit: 7)
        {
        }

        public override IEnumerable<Card> CleanInactiveCards()
        {
            return RemoveIf(m => m.Dead);
        }

        public bool HasAnyActiveTaunt()
        {
            return Cards.Any(c => c.Taunt.Value && !c.Stealth.Value);
        }
    }
}
