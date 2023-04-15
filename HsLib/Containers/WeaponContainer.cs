using HsLib.Cards;
using HsLib.Common.Place;
using HsLib.Containers.Base;

namespace HsLib.Containers
{
    public class WeaponContainer : SingleContainer<Weapon>
    {
        public WeaponContainer(Battlefield bf, Pid pid, Weapon card) : base(bf, pid, Loc.Weapon, card)
        {
        }
    }
}
