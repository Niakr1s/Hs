using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types;
using HsLib.Types.Cards;

namespace HsLib.Cards.Spells
{
    public class HolySmite : Spell
    {
        public HolySmite() : base(1)
        {
        }

        public override void UseEffect(Battlefield bf, Card? target)
        {
            if (target is IDamageable m)
            {
                m.GetDamage(2);
            }
        }

        private readonly Target _target = new Target
        {
            Locs = Loc.Field | Loc.Hero,
            Sides = PidSide.He | PidSide.Me
        };

        public override bool EffectIsSoloTarget => true;

        public override IEnumerable<Card> UseEffectTargets(Battlefield bf)
        {
            return _target.GetValidTargets(this, bf.Cards);
        }
    }
}
