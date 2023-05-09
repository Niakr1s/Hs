using HsLib.Battle;
using HsLib.Cards.Effects;

namespace HsLib.Cards
{
    public abstract class Spell : Card, IEffect
    {
        protected Spell(int mp) : base(mp)
        {
        }

        public abstract bool CanUseEffect(Battlefield bf);
        public abstract void UseEffect(Battlefield bf, Card? target);
        public abstract IEnumerable<Card> UseEffectTargets(Battlefield bf);
    }
}
