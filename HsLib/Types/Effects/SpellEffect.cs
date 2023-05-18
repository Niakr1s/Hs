using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Choosers;
using HsLib.Types.Places;

namespace HsLib.Types.Effects
{
    public class SpellEffect : PlayerEffect
    {
        public SpellEffect(IEffect effect,
            IChooser<Pid>? possibleTargetsChooser = null, IChooser<Pid>? targetsChooser = null)
            : base(effect, possibleTargetsChooser, targetsChooser)
        {
        }

        public override void ValidateEffectTarget(Battlefield bf, Pid ownerPid, ICard? effectTarget)
        {
            base.ValidateEffectTarget(bf, ownerPid, effectTarget);
            if (GetPossibleTargets(bf, ownerPid).Any() && effectTarget is null)
            {
                throw new ValidationException("spell must have any effect target even though it have none possible targets");
            }
        }
    }
}