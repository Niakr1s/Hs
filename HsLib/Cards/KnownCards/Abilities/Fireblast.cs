using HsLib.Battle;
using HsLib.Cards.Effects;
using HsLib.Common.Interfaces;
using HsLib.Common.Place;

namespace HsLib.Cards.KnownCards.Abilities
{
    public class Fireblast : Ability
    {
        public Fireblast() : base(2)
        {
        }

        private Target _effectTarget = new Target
        {
            Locs = new() { Loc.Field, Loc.Hero },
            Sides = new() { PidSide.Me, PidSide.He },
        };

        public override void UseEffect(Battlefield bf, Card owner, Card? target)
        {
            if (_effectTarget.IsValidTarget(this, target) && target is IDamageable d)
            {
                d.GetDamage(1);
            }
            else
            {
                throw new EffectWrongTargetException();
            }
        }
    }
}
