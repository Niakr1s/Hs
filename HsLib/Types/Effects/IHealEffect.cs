﻿using HsLib.Types.Stats;

namespace HsLib.Types.Effects
{
    public interface IHealEffect : IEffect
    {
        IntStat HealAmount { get; }
    }
}