﻿using HsLib.Interfaces;
using HsLib.Interfaces.CardTraits;
using HsLib.Systems;
using HsLib.Types.Effects.Base;

namespace HsLib.Types.Cards
{
    public abstract class Spell : Card, IPlayableFromHand
    {
        protected Spell(int mp) : base(mp)
        {
        }

        public SpellEffect SpellEffect { get; } = new SpellEffect();

        public Action PlayFromHand(Battlefield bf, int? fieldIndex = null, ICard? effectTarget = null)
        {
            SpellEffect.ValidatePlayFromHandEffectTarget(bf, PlaceInContainer!.Pid, effectTarget);

            Action spellEffectAction = SpellEffect.UseEffect(bf, PlaceInContainer!.Pid, effectTarget);
            return () =>
            {
                spellEffectAction();
                bf[PlaceInContainer!.Pid].Hand.Remove(this);
            };
        }
    }
}
