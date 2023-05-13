using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types.Effects;

namespace HsLib.Types.Cards
{
    public abstract class Ability : Card, IActiveEffect
    {
        protected Ability(int mp) : base(mp)
        {
        }

        protected abstract IEffect Effect { get; }

        public virtual IEnumerable<Card> UseEffectTargets(Battlefield bf) => Effect.UseEffectTargets(bf);

        public EffectType EffectType => Effect.EffectType;

        private bool EffectUsedThisTurn { get; set; }

        public void UseEffect(Battlefield bf, Card? target)
        {
            Effect.UseEffect(bf, target);
            EffectUsedThisTurn = true;
        }

        public bool CanUseEffect(Battlefield bf)
        {
            return !EffectUsedThisTurn;
        }

        public override void OnTurnStart(Battlefield bf)
        {
            base.OnTurnStart(bf);
            EffectUsedThisTurn = false;
        }

        protected sealed override void DoPlayFromHand(Battlefield bf, int? fieldIndex = null, Card? effectTarget = null)
        {
            throw new InvalidOperationException();
        }
    }
}
