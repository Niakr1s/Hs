﻿using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types;
using HsLib.Types.Cards;

namespace HsLib.Cards.Abilities
{
    public class Fireblast : Ability
    {
        public Fireblast() : base(2)
        {
            EffectTargets = new Target
            {
                Locs = new() { Loc.Field, Loc.Hero },
                Sides = new() { PidSide.Me, PidSide.He },
            };
        }

        public override bool EffectMustHaveTarget => true;

        protected override void DoUseEffect(Battlefield bf, Card? target)
        {
            if (target is IDamageable d)
            {
                d.GetDamage(1);
            }
        }
    }
}