using HsLib.Cards;
using HsLib.Common.Place;
using HsLib.Containers.Base;

namespace HsLib.Containers
{
    public class WeaponC : SingleContainer<Weapon>
    {
        public WeaponC(Battlefield bf, Pid pid, Weapon card) : base(bf, pid, Loc.Weapon, card)
        {
        }
    }
}
