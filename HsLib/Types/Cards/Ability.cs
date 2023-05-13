using HsLib.Interfaces;
using HsLib.Systems;

namespace HsLib.Types.Cards
{
    public abstract class Ability : Card, IActiveEffect
    {
        protected Ability(int mp) : base(mp)
        {
        }

        protected abstract IEffect Effect { get; }

        public virtual IEnumerable<Card> UseEffectTargets(Battlefield bf) => Effect.UseEffectTargets(bf);

        public bool EffectIsSoloTarget => Effect.EffectIsSoloTarget;

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
    }
}
