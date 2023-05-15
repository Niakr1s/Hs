﻿using HsLib.Interfaces;
using HsLib.Types.Cards;
using HsLib.Types.Effects;

namespace HsLib.Cards.Spells
{
    public class MindControl : Spell
    {
        public MindControl() : base(10)
        {
            SpellEffect = new MindControlTargetEffect();
        }

        public override ITargetEffect SpellEffect { get; }
    }
}
