
using HsLib.Interfaces;
using HsLib.Systems;

namespace HsLib.Types.Effects.Base
{
    public class BattlecryEffect : PlayableFromHandActiveEffect
    {
        public BattlecryEffect(IActiveEffect? activeEffect = null) : base(activeEffect)
        {
        }

        public override void ValidatePlayFromHandEffectTarget(Battlefield bf, Pid pid, ICard? effectTarget)
        {
            // todo: make tests
            if (ActiveEffect is not null)
            {
                if (effectTarget is null)
                {
                    if (ActiveEffect.GetPossibleTargets(bf, pid).Any())
                    {
                        throw new ValidationException("effect target is null even though some possible targets are present");
                    }
                }
                else
                {
                    if (!ActiveEffect.GetPossibleTargets(bf, pid).Contains(effectTarget))
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