﻿using HsLib.Types.Cards;
using HsLib.Types.Choosers;
using HsLib.Types.Effects;
using HsLib.Types.Places;

namespace HsLib.KnownCards.Spells
{
    public class HolySmite : Spell
    {
        public HolySmite() : base(1)
        {
            IEffect effect = new DamageEffect(2);
            Targets possibleTargets = new Targets { Locs = Loc.Field | Loc.Hero, Sides = Side.He | Side.Me };
            SpellEffect = new(this, effect, possibleTargetsChooser: possibleTargets);
        }

        public override SpellEffect SpellEffect { get; }
    }
}
