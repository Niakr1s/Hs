﻿using HsLib.Types.Cards;
using HsLib.Types.Choosers;
using HsLib.Types.Effects;

namespace HsLib.KnownCards.Abilities
{
    public class ArmorUp : Ability
    {
        public ArmorUp() : base(2)
        {
            GetArmorEffect effect = new() { Armor = 2 };
            AbilityEffect = new(this, effect, targetsChooser: new HeroChooser());
        }

        public override AbilityEffect AbilityEffect { get; }
    }
}
