using HsLib.Battle;
using HsLib.Cards.Effects;
using HsLib.Common.Place;
using HsLib.Stats;

namespace HsLib.Cards
{
    public abstract class Spell : Card, IEffect
    {
        protected Spell(int mp) : base(mp)
        {
        }

        public abstract bool EffectMustHaveTarget { get; }

        public bool CanUseEffect(Battlefield bf)
        {
            if (!bf.Turn.IsActivePid(Pid)) { return false; }
            if (Loc != Loc.Hand) { return false; }
            if (bf[Pid].Mp.Value < Mp.Value) { return false; }
            return true;
        }

        public abstract void UseEffect(Battlefield bf, Card? target);

        public abstract IEnumerable<Card> UseEffectTargets(Battlefield bf);

        public override void PlayFromHand(Battlefield bf, int? fieldIndex = null, Card? effectTarget = null)
        {
            base.PlayFromHand(bf);
            bf.BattleService.UseEffect(this, effectTarget);
            bf.MoveService.MoveHandToGraveyard(Pid, Index);
        }
    }
}
