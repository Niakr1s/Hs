using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Choosers;
using HsLib.Types.Places;

namespace HsLib.Types.Effects
{
    public abstract class PlayerEffect : TargetableEffect<Pid>
    {
        protected PlayerEffect(ICard owner, IEffect effect,
            IChooser<Pid>? possibleTargetsChooser = null, IChooser<Pid>? targetsChooser = null)
            : base(owner, effect, possibleTargetsChooser, targetsChooser)
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
