using HsLib.Cards.Effects;
using HsLib.Containers;

namespace HsLib.Cards
{
    public abstract class Ability : Card, IEffect
    {
        protected Ability(int mp) : base(mp)
        {

        }

        public abstract void UseEffect(Battlefield bf, Card owner, Card? target);
    }
}
