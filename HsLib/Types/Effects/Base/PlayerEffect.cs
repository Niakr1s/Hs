
using HsLib.Interfaces;
using HsLib.Systems;

namespace HsLib.Types.Effects.Base
{
    public abstract class PlayerEffect : ActiveEffect<Pid>
    {
        protected PlayerEffect(IEffect effect,
            ICardsChooser<Pid>? possibleTargetsChooser = null, ICardsChooser<Pid>? targetsChooser = null)
            : base(effect, possibleTargetsChooser, targetsChooser)
        {
        }

        public override void ValidateEffectTarget(Battlefield bf, Pid owner, ICard? effectTarget)
        {
            if (effectTarget is null)
            {
                if (GetPossibleTargets(bf, owner).Any())
                {
                    throw new ValidationException("effect target is null even though some possible targets are present");
                }
            }
            else
            {
                if (!GetPossibleTargets(bf, owner).Contains(effectTarget))
                {
                    throw new ValidationException(
                    "effect target is not null even though no possible targets are present");
                }
            }
        }
    }
}
