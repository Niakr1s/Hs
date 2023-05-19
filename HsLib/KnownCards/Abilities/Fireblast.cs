﻿using HsLib.Types.Cards;
using HsLib.Types.Choosers;
using HsLib.Types.Effects;
using HsLib.Types.Places;

namespace HsLib.KnownCards.Abilities
{
    public class Fireblast : Ability
    {

        public Fireblast() : base(2)
        {
            DealDamageEffect effect = new() { Damage = 1 };
            Targets possibleTargets = new() { Locs = Loc.Field | Loc.Hero, Sides = PidSide.Me | PidSide.He };
            AbilityEffect = new(effect, possibleTargetsChooser: possibleTargets);
        }

        public override AbilityEffect AbilityEffect { get; }
    }
}