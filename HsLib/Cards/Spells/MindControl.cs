using HsLib.Systems;
using HsLib.Types;
using HsLib.Types.Cards;
using HsLib.Types.Effects;

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

        private readonly Targets _targets = new Targets { Locs = Loc.Field, Sides = PidSide.He };

        public override EffectType EffectType => EffectType.Solo;

        public override IEnumerable<Card> UseEffectTargets(Battlefield bf)
        {
            return _targets.GetValidTargets(this, bf.Cards);
        }
    }
}
