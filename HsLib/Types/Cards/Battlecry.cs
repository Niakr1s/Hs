using HsLib.Interfaces;
using HsLib.Systems;

namespace HsLib.Types.Cards
{
    public abstract class Battlecry : IActiveEffect
    {
        protected Battlecry(Card owner)
        {
            Owner = owner;
        }

        public Card Owner { get; }

        public bool EffectIsSoloTarget => Effect.EffectIsSoloTarget;

        public void UseEffect(Battlefield bf, Card? target) => Effect.UseEffect(bf, target);

        public IEnumerable<Card> UseEffectTargets(Battlefield bf) => Effect.UseEffectTargets(bf);

        public bool CanUseEffect(Battlefield bf) => true;

        protected abstract IEffect Effect { get; }
    }
}
