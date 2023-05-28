using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Places;

namespace HsLib.Types.Containers
{
    public class WeaponContainer : SingleMaybeContainer<Weapon>
    {
        public WeaponContainer(Battlefield bf, Pid pid, Weapon? startCard = null) : base(bf, new(pid, Loc.Weapon))
        {
            Card = startCard;
        }
    }
}