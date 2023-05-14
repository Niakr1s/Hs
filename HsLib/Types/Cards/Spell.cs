using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types.Effects;

namespace HsLib.Types.Cards
{
    public abstract class Spell : Card, IEffect
    {
        protected Spell(int mp) : base(mp)
        {
        }

        public abstract EffectType EffectType { get; }

        public bool CanUseEffect(Battlefield bf)
        {
            return Place?.Loc == Loc.Hand;
        }

        public abstract void UseEffect(Battlefield bf, Pid pid, Card? target);

        public abstract IEnumerable<Card> UseEffectTargets(Battlefield bf, Pid pid);

        protected override void DoPlayFromHand(Battlefield bf, int? fieldIndex = null, Card? effectTarget = null)
        {
            if (Place is null) { return; }

            bf.BattleService.UseEffect(this, Place.Pid, effectTarget);
            bf.MoveService.RemoveCard(Place);
        }
    }
}
