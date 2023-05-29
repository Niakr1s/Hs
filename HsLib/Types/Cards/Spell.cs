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

        public Action PlayFromHand(IBoard board, int? fieldIndex = null, ICard? effectTarget = null)
        {
            TargetableEffectValidator.ValidateEffectTarget(SpellEffect, board, effectTarget);

            Action spellEffectAction = SpellEffect.UseEffect(board, effectTarget);
            return () =>
            {
                spellEffectAction();
                board[Place.Pid].Hand.Remove(this);
            };
        }
    }
}
