using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Containers.Base;

namespace HsLib.Types.Containers
{
    public class AbilityC : SingleContainer<Ability>
    {
        public AbilityC(Battlefield bf, Pid pid, Ability card) : base(bf, pid, Loc.Ability, card)
        {
        }

        public override IEnumerable<Card> CleanInactiveCards()
        {
            yield break;
        }
    }
}
