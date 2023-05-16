
using HsLib.Interfaces;
using HsLib.Systems;


namespace HsLib.Types.Effects.Base
{
    public class SpellEffect : BattlecryEffect
    {
        public SpellEffect(IActiveEffect? activeEffect = null) : base(activeEffect)
        {
        }

        public override void ValidatePlayFromHandEffectTarget(Battlefield bf, Pid pid, ICard? effectTarget)
        {
            base.ValidatePlayFromHandEffectTarget(bf, pid, effectTarget);
            if (ActiveEffect?.GetPossibleTargets(bf, pid).Any() == true && effectTarget is null)
            {
                throw new ValidationException("spell must have any effect target even though it have none possible targets");
            }
        }
    }
}