using HsLib.Battle;
using HsLib.Cards.Effects;

namespace HsLib.Cards
{
    public abstract class Ability : Card, IEffect
    {
        protected Ability(int mp) : base(mp)
        {

        }

        public virtual bool CanBeUsedThisTurn { get; set; }

        public abstract void UseEffect(Battlefield bf, Card owner, Card? target);

        public override void OnTurnStart(Battlefield bf)
        {
            base.OnTurnStart(bf);
            CanBeUsedThisTurn = true;
        }
    }
}
