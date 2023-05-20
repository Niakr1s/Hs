﻿using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Stats;

namespace HsLib.Types.LingeringEffects.Auras
{
    public interface IAuraEffect
    {
        IEnchantHandler GiveAura(Battlefield bf, ICard owner, ICard target);
    }
}