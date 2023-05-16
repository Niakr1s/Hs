using HsLib.Exceptions;
using HsLib.Interfaces;
using HsLib.Interfaces.CardTraits;
using HsLib.Systems;
using HsLib.Validators;

namespace HsLib.Types.Cards
{
    public abstract class Spell : Card, IPlayableFromHand
    {
        protected Spell(int mp) : base(mp)
        {
        }

        public abstract IActiveEffect SpellEffect { get; }

        public Action PlayFromHand(Battlefield bf, int? fieldIndex = null, ICard? effectTarget = null)
        {
            PlayableFromHandValidators.ValidateEffectTarget(bf, Place!.Pid, effectTarget, SpellEffect);
            AdditionalValidate(bf, effectTarget);

            Action spellEffectAction = SpellEffect.UseEffect(bf, Place!.Pid, effectTarget);
            return () =>
            {
                spellEffectAction();
                bf[Place.Pid].Hand.Remove(this);
            };
        }

        private void AdditionalValidate(Battlefield bf, ICard? effectTarget = null)
        {
            if (!SpellEffect.GetPossibleTargets(bf, Place!.Pid).Any() && effectTarget is null)
            {
                throw new ValidationException("spell must have any effect target even though it have none possible targets");
            }
        }
    }
}
