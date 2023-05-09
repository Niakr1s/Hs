using HsLib.Battle;
using HsLib.Common.Place;

namespace HsLib.Cards.Effects
{
    public abstract class Battlecry : IEffect
    {
        protected Battlecry(Minion minion)
        {
            Minion = minion;
        }

        public Minion Minion { get; }

        protected Target? EffectTargets { get; set; }

        public IEnumerable<Card> UseEffectTargets(Battlefield bf)
        {
            if (!CanUseEffect(bf)) { yield break; }

            foreach (Card card in bf.Cards)
            {
                if (EffectTargets?.IsValidTarget(Minion, card) == true)
                {
                    yield return card;
                }
            }
        }

        public abstract void UseEffect(Battlefield bf, Card? target);

        public bool CanUseEffect(Battlefield bf)
        {
            return EffectTargets is not null;
        }
    }
}
