using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types.Effects;

namespace HsLib.Types.Cards
{
    public abstract class Battlecry : IEffect
    {
        protected Battlecry(Card owner)
        {
            Owner = owner;
        }

        public Card Owner { get; }

        public EffectType EffectType => Effect.EffectType;

        public void UseEffect(Battlefield bf, Card? target) => Effect.UseEffect(bf, target);

        public IEnumerable<Card> UseEffectTargets(Battlefield bf) => Effect.UseEffectTargets(bf);

        protected abstract IEffect Effect { get; }
    }
}
