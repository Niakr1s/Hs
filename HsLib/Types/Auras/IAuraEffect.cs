﻿using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Stats;

namespace HsLib.Types.Auras
{
    public interface IAuraEffect
    {
        IEnchantHandler GiveAura(IBoard board, ICard owner, ICard target);
    }
}
