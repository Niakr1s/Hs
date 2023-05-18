using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.CardsChoosers;
using HsLib.Types.Places;

namespace HsLib.Types.Effects
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