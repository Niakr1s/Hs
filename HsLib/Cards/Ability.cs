using HsLib.Battle;
using HsLib.Cards.Effects;
using HsLib.Common.Place;

namespace HsLib.Cards
{
    public abstract class Ability : Card, IEffect
    {
        protected Ability(int mp) : base(mp)
        {

        }

        protected Target? EffectTargets { get; set; }

        public virtual IEnumerable<Card> UseEffectTargets(Battlefield bf)
        {
            if (EffectTargets is null) { yield break; }

            foreach (Card card in bf.Cards)
            {
                if (EffectTargets?.IsValidTarget(this, card) == true)
                {
                    yield return card;
                }
            }
        }

        public abstract bool EffectMustHaveTarget { get; }

        private bool EffectUsedThisTurn { get; set; }

        public void UseEffect(Battlefield bf, Card? target)
        {
            DoUseEffect(bf, target);
            EffectUsedThisTurn = true;
        }

        protected abstract void DoUseEffect(Battlefield bf, Card? target);

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
