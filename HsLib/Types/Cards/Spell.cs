﻿using HsLib.Systems;
using HsLib.Types.Effects;

namespace HsLib.Types.Cards
{
    public abstract class Spell : Card, IPlayableFromHand
    {
        protected Spell(int mp) : base(mp)
        {
        }

        public abstract SpellEffect SpellEffect { get; }

        public Action PlayFromHand(Battlefield bf, int? fieldIndex = null, ICard? effectTarget = null)
        {
            ActiveEffectValidator.ValidateEffectTarget(SpellEffect, bf, PlaceInContainer!.Pid, effectTarget);

            Action spellEffectAction = SpellEffect.UseEffect(bf, PlaceInContainer!.Pid, effectTarget);
            return () =>
            {
                spellEffectAction();
                bf[PlaceInContainer!.Pid].Hand.Remove(this);
            };
        }
    }
}
