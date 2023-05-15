using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types.Effects.Base;

namespace HsLib.Types.Cards
{
    public abstract class Battlecry : ITargetEffect
    {
        protected Battlecry(Card owner)
        {
            Owner = owner;
        }

        public Card Owner { get; }

        public EffectType EffectType => Effect.EffectType;

        public void UseEffect(Battlefield bf, Pid pid, ICard? target) => Effect.UseEffect(bf, pid, target);

        public IEnumerable<ICard> UseEffectTargets(Battlefield bf, Pid pid) => Effect.UseEffectTargets(bf, pid);

        protected abstract ITargetEffect Effect { get; }
    }
}
