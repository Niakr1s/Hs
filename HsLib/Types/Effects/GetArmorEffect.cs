﻿using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLib.Types.Effects
{
    public class GetArmorEffect : TargetEffect
    {
        public GetArmorEffect(Card owner) : base(owner, EffectType.Self)
        {
        }

        public int Armor { get; set; }

        protected override void EffectAction(Battlefield bf, ICard? card)
        {
            if (Owner.Place is null) { return; }
            bf[Owner.Place.Pid].Hero.Card.Armor.Increase(2);
        }
    }
}
