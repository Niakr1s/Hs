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

        public virtual bool CanBeUsedThisTurn { get; set; }

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

        public abstract void UseEffect(Battlefield bf, Card? target);

        public override void OnTurnStart(Battlefield bf)
        {
            base.OnTurnStart(bf);
            CanBeUsedThisTurn = true;
        }

    }
}
