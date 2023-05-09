using HsLib.Battle;
using HsLib.Common.Interfaces;
using HsLib.Common.Place;

namespace HsLib.Cards.KnownCards.Abilities
{
    public class Fireblast : Ability
    {
        public Fireblast() : base(2)
        {
            EffectTargets = new Target
            {
                Locs = new() { Loc.Field, Loc.Hero },
                Sides = new() { PidSide.Me, PidSide.He },
            };
        }

        protected override void DoUseEffect(Battlefield bf, Card? target)
        {
            if (target is IDamageable d)
            {
                d.GetDamage(1);
            }
        }
    }
}
