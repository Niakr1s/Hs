using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types.Effects;

namespace HsLib.Types.Cards
{
    public abstract class Spell : Card, IActiveEffect
    {
        protected Spell(int mp) : base(mp)
        {
        }

        public abstract EffectType EffectType { get; }

        public bool CanUseEffect(Battlefield bf)
        {
            if (Place is null) { return false; }
            if (!bf.Turn.IsActivePid(Place.Pid)) { return false; }
            if (Place.Loc != Loc.Hand) { return false; }
            if (bf[Place.Pid].Mp.Value < Mp.Value) { return false; }
            return true;
        }

        public abstract void UseEffect(Battlefield bf, Card? target);

        public abstract IEnumerable<Card> UseEffectTargets(Battlefield bf);

        protected override void DoPlayFromHand(Battlefield bf, int? fieldIndex = null, Card? effectTarget = null)
        {
            if (Place is null) { return; }

            bf.BattleService.UseActiveEffect(this, effectTarget);
            bf.MoveService.RemoveCard(Place);
        }
    }
}
