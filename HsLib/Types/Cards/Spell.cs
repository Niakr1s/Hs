using HsLib.Interfaces;
using HsLib.Systems;

namespace HsLib.Types.Cards
{
    public abstract class Spell : Card, IEffect
    {
        protected Spell(int mp) : base(mp)
        {
        }

        public abstract bool EffectMustHaveTarget { get; }

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

        public override void PlayFromHand(Battlefield bf, int? fieldIndex = null, Card? effectTarget = null)
        {
            if (Place is null) { return; }

            base.PlayFromHand(bf);
            bf.BattleService.UseEffect(this, effectTarget);
            bf.MoveService.MoveHandToGraveyard(Place.Pid, Place.Index);
        }
    }
}
