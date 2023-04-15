﻿using HsLib.Cards.Effects;
using HsLib.Common.Place;
using HsLib.Containers;
using HsLib.Services.Battle;

namespace HsLib.Cards.KnownCards.Abilities
{
    public class Fireblast : Ability
    {
        public Fireblast() : base(2)
        {
        }

        private Target _effectTarget = new Target
        {
            Container = TargetContainer.Field | TargetContainer.Hero,
            Side = TargetSide.Me | TargetSide.He
        };

        public override void UseEffect(Battlefield bf, Card owner, Card? target)
        {
            if (_effectTarget.IsValidTarget(this, target) && target is IDamageable d)
            {
                d.GetDamage(2);
            }
            else
            {
                throw new EffectWrongTargetException();
            }
        }
    }
}
