using HsLib.Battle;
using HsLib.Common.MeleeAttack;
using HsLib.Common.Place;

namespace HsLib.Cards.KnownCards.Spells
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
            Locs = new() { Loc.Field, Loc.Hero },
            Sides = new() { PidSide.He, PidSide.Me }
        };

        public override bool EffectMustHaveTarget => true;

        public override IEnumerable<Card> UseEffectTargets(Battlefield bf)
        {
            return _target.GetValidTargets(this, bf.Cards);
        }
    }
}
