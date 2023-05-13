using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLib.Types.Effects
{
    public abstract class Effect : IEffect
    {
        protected Effect(Card owner, bool isSoloTarget = false, Target targets = new Target())
        {
            Owner = owner;
            EffectIsSoloTarget = isSoloTarget;
            _targets = targets;
        }

        public Card Owner { get; }

        public bool EffectIsSoloTarget { get; }

        protected abstract void EffectAction(Battlefield bf, Card? card);

        private readonly Target _targets;

        public virtual IEnumerable<Card> UseEffectTargets(Battlefield bf)
        {
            return _targets.GetValidTargets(Owner, bf.Cards);
        }

        public void UseEffect(Battlefield bf, Card? target)
        {
            if (EffectIsSoloTarget)
            {
                UseEffectSolo(bf, target);
            }
            else
            {
                UseEffectMass(bf);
            }
        }

        private void UseEffectSolo(Battlefield bf, Card? target)
        {
            EffectAction(bf, target);
        }

        private void UseEffectMass(Battlefield bf)
        {
            EffectAction(bf, null);
        }
    }
}
