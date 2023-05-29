using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Places;

namespace HsLib.Types.Containers
{
    public class WeaponContainer : SingleMaybeContainer<Weapon>
    {
        public WeaponContainer(IBoard board, Pid pid, Weapon? startCard = null) : base(board, new(pid, Loc.Weapon))
        {
            Card = startCard;
        }
    }
}