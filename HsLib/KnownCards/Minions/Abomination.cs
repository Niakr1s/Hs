﻿using HsLib.Types.Cards;
using HsLib.Types.Choosers;
using HsLib.Types.Effects;
using HsLib.Types.Places;

namespace HsLib.KnownCards.Minions
{
    public class Abomintaion : Minion
    {
        public Abomintaion() : base(5, 4, 4)
        {
            DamageEffect effect = new() { Damage = 2 };
            Targets targetsChooser = new Targets { Locs = Loc.Field | Loc.Hero, Sides = PidSide.Me | PidSide.He, };
            DeathrattleEffect = new(effect, targetsChooser);
        }
    }
}
