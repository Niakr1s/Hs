
using HsLib.Interfaces;
using HsLib.Systems;

namespace HsLib.Types.Effects.Base
{
    public class AbilityEffect : PlayerEffect
    {
        public AbilityEffect(IEffect effect,
            ICardsChooser<Pid>? possibleTargetsChooser = null, ICardsChooser<Pid>? targetsChooser = null)
            : base(effect, possibleTargetsChooser, targetsChooser)
        {
        }
        public override void ValidateEffectTarget(Battlefield bf, Pid ownerPid, ICard? effectTarget)
        {
            base.ValidateEffectTarget(bf, ownerPid, effectTarget);
            if (GetPossibleTargets(bf, ownerPid).Any() && effectTarget is null)
            {
                throw new ValidationException("ability must have any effect target even though it have none possible targets");
            }
        }
    }
}