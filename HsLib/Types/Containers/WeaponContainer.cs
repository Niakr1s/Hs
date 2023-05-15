using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Containers.Base;

namespace HsLib.Types.Containers
{
    public class WeaponContainer : SingleContainer<Weapon>
    {
        public WeaponContainer(Battlefield bf, Pid pid, Weapon card) : base(bf, new Place(pid, Loc.Weapon), card)
        {
        }

        protected override bool IsCardActive(ICard card)
        {
            return !((Weapon)card).Dead;
        }
    }
}
