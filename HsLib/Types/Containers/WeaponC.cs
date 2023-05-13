using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Containers.Base;
using HsLib.Cards.Weapons;

namespace HsLib.Types.Containers
{
    public class WeaponC : SingleContainer<Weapon>
    {
        public WeaponC(Battlefield bf, Pid pid, Weapon? card = null) : base(bf, pid, Loc.Weapon, card ?? new NoWeapon())
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
