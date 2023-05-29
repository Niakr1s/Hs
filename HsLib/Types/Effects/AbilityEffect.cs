using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Choosers;

namespace HsLib.Types.Effects
{
    public class AbilityEffect : PlayerEffect
    {
        public AbilityEffect(ICard owner, IEffect effect,
            IChooser<ICard>? possibleTargetsChooser = null, IChooser<ICard>? targetsChooser = null)
            : base(owner, effect, possibleTargetsChooser, targetsChooser)
        {
        }
        public override void ValidateEffectTarget(IBoard board, ICard? effectTarget)
        {
            base.ValidateEffectTarget(board, effectTarget);
            if (GetPossibleTargets(board).Any() && effectTarget is null)
            {
                throw new ValidationException("ability must have any effect target even though it have none possible targets");
            }
        }
    }
}