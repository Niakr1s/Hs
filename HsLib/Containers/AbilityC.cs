using HsLib.Battle;
using HsLib.Cards;
using HsLib.Common.Place;
using HsLib.Containers.Base;

namespace HsLib.Containers
{
    public class AbilityC : SingleContainer<Ability>
    {
        public AbilityC(Battlefield bf, Pid pid, Ability card) : base(bf, pid, Loc.Ability, card)
        {
        }
    }
}
