﻿
using HsLib.Interfaces;
using HsLib.Systems;


namespace HsLib.Types.Effects.Base
{
    public class AbilityEffect : BattlecryEffect
    {
        public AbilityEffect(IActiveEffect<Pid> activeEffect) : base(activeEffect)
        {
        }

        public override void ValidatePlayFromHandEffectTarget(Battlefield bf, Pid ownerPid, ICard? effectTarget)
        {
            base.ValidatePlayFromHandEffectTarget(bf, ownerPid, effectTarget);
            if (Effect?.GetPossibleTargets(bf, ownerPid).Any() == true && effectTarget is null)
            {
                throw new ValidationException("ability must have any effect target even though it have none possible targets");
            }
        }
    }
}