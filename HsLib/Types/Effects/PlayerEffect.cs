using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Choosers;

namespace HsLib.Types.Effects
{
    public abstract class PlayerEffect : TargetableEffect
    {
        protected PlayerEffect(ICard owner, IEffect effect,
            IChooser<ICard>? possibleTargetsChooser = null, IChooser<ICard>? targetsChooser = null)
            : base(owner, effect, possibleTargetsChooser, targetsChooser)
        {
        }

        public override void ValidateEffectTarget(Board board, ICard? effectTarget)
        {
            if (effectTarget is null)
            {
                if (GetPossibleTargets(board).Any())
                {
                    throw new ValidationException("effect target is null even though some possible targets are present");
                }
            }
            else
            {
                if (!GetPossibleTargets(board).Contains(effectTarget))
                {
                    throw new ValidationException(
                    "effect target is not null even though no possible targets are present");
                }
            }
        }
    }
}
