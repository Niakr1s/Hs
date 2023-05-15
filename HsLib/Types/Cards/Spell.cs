﻿using HsLib.Interfaces;
using HsLib.Systems;

namespace HsLib.Types.Cards
{
    public abstract class Spell : Card, IPlayableFromHand
    {
        protected Spell(int mp) : base(mp)
        {
        }

        public abstract ITargetEffect SpellEffect { get; }

        public void PlayFromHand(Battlefield bf, int? fieldIndex = null, ICard? effectTarget = null)
        {
            SpellEffect.UseEffect(bf, Place!.Pid, effectTarget);
            bf[Place.Pid].Hand.Remove(this);
        }
    }
}
