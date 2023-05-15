﻿using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLib.Types.Effects
{
    public class DealDamageEffect : IEffect
    {
        public int Damage { get; set; }

        public Action UseEffect(Battlefield bf, ICard target)
        {
            if (target is IDamageable d)
            {
                return () => bf.BattleService.DealDamage(Damage, d);
            }
            else
            {
                throw new ArgumentException("target can't be damaged");
            }
        }
    }
}
