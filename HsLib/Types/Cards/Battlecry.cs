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

        public void UseEffect(Battlefield bf, Pid pid, Card? target) => Effect.UseEffect(bf, pid, target);

        public IEnumerable<Card> UseEffectTargets(Battlefield bf, Pid pid) => Effect.UseEffectTargets(bf, pid);

        protected abstract IEffect Effect { get; }
    }
}
