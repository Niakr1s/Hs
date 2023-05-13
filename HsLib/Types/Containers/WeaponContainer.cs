using HsLib.Cards.Weapons;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Containers.Base;

namespace HsLib.Types.Containers
{
    public class WeaponContainer : SingleContainer<Weapon>
    {
        public WeaponContainer(Battlefield bf, Pid pid, Weapon? card = null) : base(bf, pid, Loc.Weapon, card ?? new NoWeapon())
        {
        }

        public override IEnumerable<Card> CleanInactiveCards()
        {
            if (Card is NoWeapon) { yield break; }
            Weapon removedWeapon = Card;
            Card = new NoWeapon();
            yield return removedWeapon;
        }
    }
}
