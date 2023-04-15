using Models.Cards;
using Models.Common.Place;
using Models.Containers;
using Models.Containers.Base;

namespace HsLib.Containers
{
    public class WeaponContainer : SingleContainer<Weapon>
    {
        public WeaponContainer(Battlefield bf, Pid pid, Weapon card) : base(bf, pid, Loc.Weapon, card)
        {
        }
    }
}
