using HsLib.Systems;
using HsLib.Types.Effects;

namespace HsLib.Types.Cards
{
    public abstract class Spell : Card, IPlayableFromHand
    {
        protected Spell(int mp) : base(mp)
        {
        }

        public abstract SpellEffect SpellEffect { get; }

        public Action PlayFromHand(Battlefield bf, int? fieldIndex = null, ICard? effectTarget = null)
        {
            TargetableEffectValidator.ValidateEffectTarget(SpellEffect, bf, effectTarget);

            Action spellEffectAction = SpellEffect.UseEffect(bf, effectTarget);
            return () =>
            {
                spellEffectAction();
                bf[Place.Pid].Hand.Remove(this);
            };
        }
    }
}
