
using HsLib.Interfaces;
using HsLib.Systems;

namespace HsLib.Types.Effects.Base
{
    public class BattlecryEffect : PlayableFromHandEffect
    {
        public BattlecryEffect(IActiveEffect<Pid>? pidEffect = null) : base(pidEffect)
        {
        }

        public override void ValidatePlayFromHandEffectTarget(Battlefield bf, Pid ownerPid, ICard? effectTarget)
        {
            if (Effect is not null)
            {
                if (effectTarget is null)
                {
                    if (Effect.GetPossibleTargets(bf, ownerPid).Any())
                    {
                        throw new ValidationException("effect target is null even though some possible targets are present");
                    }
                }
                else
                {
                    if (!Effect.GetPossibleTargets(bf, ownerPid).Contains(effectTarget))
                    {
                        throw new ValidationException(
                        "effect target is not null even though no possible targets are present");
                    }
                }
            }
            else
            {
                if (effectTarget is not null)
                {
                    throw new ValidationException("effect target is not null even though effect is null");
                }
            }
        }
    }
}