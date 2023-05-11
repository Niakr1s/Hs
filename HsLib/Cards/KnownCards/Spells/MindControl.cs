using HsLib.Battle;
using HsLib.Common.Place;

namespace HsLib.Cards.KnownCards.Spells
{
    public class MindControl : Spell
    {
        public MindControl() : base(10)
        {
        }

        public override void UseEffect(Battlefield bf, Card? target)
        {
            if (target is null) return;

            if (target is Minion m)
            {
                bf[m.Pid].Field.Remove(m);
                bf[Pid].Field.Add(m);
            }
        }

        private readonly Target _target = new Target { Locs = new() { Loc.Field }, Sides = new() { PidSide.He } };

        public override bool EffectMustHaveTarget => true;

        public override IEnumerable<Card> UseEffectTargets(Battlefield bf)
        {
            return _target.GetValidTargets(this, bf.Cards);
        }
    }
}
