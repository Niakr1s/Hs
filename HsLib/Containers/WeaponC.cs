﻿using HsLib.Battle;
using HsLib.Cards;
using HsLib.Cards.KnownCards.Weapons;
using HsLib.Common.Place;
using HsLib.Containers.Base;

namespace HsLib.Containers
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
