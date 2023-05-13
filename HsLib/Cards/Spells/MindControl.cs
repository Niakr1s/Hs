using HsLib.Systems;
using HsLib.Types;
using HsLib.Types.Cards;

namespace HsLib.Cards.Spells
{
    public class MindControl : Spell
    {
        public MindControl() : base(10)
        {
        }

        public override void UseEffect(Battlefield bf, Card? target)
        {
            if (target is null) return;

            if (target is Minion m && m.Place is not null && Place is not null)
            {
                bf[m.Place.Pid].Field.Remove(m);
                bf[Place.Pid].Field.Add(m);
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
