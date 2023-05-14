using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLib.Types.Effects
{
    public abstract class Effect : IEffect
    {
        protected Effect(Card owner, EffectType type, Targets targets = new Targets())
        {
            Owner = owner;
            EffectType = type;
            _targets = targets;
        }

        public Card Owner { get; }

        public EffectType EffectType { get; }

        protected abstract void EffectAction(Battlefield bf, ICard? card);

        private readonly Targets _targets;

        public virtual IEnumerable<ICard> UseEffectTargets(Battlefield bf, Pid pid)
        {
            return _targets.GetValidTargets(pid, bf.Cards);
        }

        public void UseEffect(Battlefield bf, Pid pid, ICard? target)
        {
            switch (EffectType)
            {
                case EffectType.Self:
                    EffectAction(bf, null);
                    break;

                case EffectType.Solo:
                    EffectAction(bf, target);
                    break;

                case EffectType.Mass:
                    foreach (var card in UseEffectTargets(bf, pid))
                    {
                        EffectAction(bf, card);
                    }
                    break;
            }
        }
    }
}
