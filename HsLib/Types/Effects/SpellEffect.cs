using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Choosers;

namespace HsLib.Types.Effects
{
    public class SpellEffect : PlayerEffect
    {
        public SpellEffect(ICard owner, IEffect effect,
            IChooser<ICard>? possibleTargetsChooser = null, IChooser<ICard>? targetsChooser = null)
            : base(owner, effect, possibleTargetsChooser, targetsChooser)
        {
        }

        public override void ValidateEffectTarget(Battlefield bf, ICard? effectTarget)
        {
            base.ValidateEffectTarget(bf, effectTarget);
            if (GetPossibleTargets(bf).Any() && effectTarget is null)
            {
                throw new ValidationException("spell must have any effect target even though it have none possible targets");
            }
        }
    }
}