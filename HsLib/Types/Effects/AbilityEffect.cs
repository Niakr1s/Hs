using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Choosers;

namespace HsLib.Types.Effects
{
    public class AbilityEffect : PlayerEffect
    {
        public AbilityEffect(ICard owner, IEffect effect,
            IChooser? possibleTargetsChooser = null, IChooser? targetsChooser = null)
            : base(owner, effect, possibleTargetsChooser, targetsChooser)
        {
        }
        public override void ValidateEffectTarget(Battlefield bf, ICard? effectTarget)
        {
            base.ValidateEffectTarget(bf, effectTarget);
            if (GetPossibleTargets(bf).Any() && effectTarget is null)
            {
                throw new ValidationException("ability must have any effect target even though it have none possible targets");
            }
        }
    }
}