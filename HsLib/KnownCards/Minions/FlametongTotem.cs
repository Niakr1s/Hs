﻿using HsLib.Types.Cards;
using HsLib.Types.Choosers;
using HsLib.Types.LingeringEffects.Auras;

namespace HsLib.KnownCards.Minions
{
    public class FlametongTotem : Minion
    {
        public FlametongTotem() : base(2, 0, 3)
        {
            AuraSource auraSource = new AuraSource(this,
                new GiveStatAuraEffect<int>(c => ((IWithAtk)c).Atk) { Value = 2 },
                new FieldAdjacentMinionsChooser());

            FieldEffectSources.Add(auraSource);
        }
    }
}
