﻿using HsLib.Battle;

namespace HsLib.Cards.KnownCards.Abilities
{
    public class ArmorUp : Ability
    {
        public ArmorUp() : base(2)
        {
            EffectTargets = null;
        }

        protected override void DoUseEffect(Battlefield bf, Card? target)
        {
            bf[Pid].Hero.Card.Armor.Increase(2);
        }
    }
}
