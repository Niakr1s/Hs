using Models.Cards;
using Models.Common.Place;
using Models.Containers.Base;

namespace HsLib.Containers
{
    public class WeaponContainer : SingleContainer<Weapon>
    {
        public WeaponContainer(Pid pid, Weapon card) : base(pid, Loc.Weapon, card)
        {
        }
    }
}
