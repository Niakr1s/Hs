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
            PlayableFromHandValidators.ValidateEffectTarget(bf, Place!.Pid, effectTarget, SpellEffect, isSpell: true);

            Action spellEffectAction = SpellEffect.UseEffect(bf, Place!.Pid, effectTarget);
            return () =>
            {
                spellEffectAction();
                bf[Place.Pid].Hand.Remove(this);
            };
        }
    }
}
