using HsLib.Interfaces;
using HsLib.Types;
using HsLib.Types.Cards;
using HsLib.Types.Effects;
using HsLib.Types.Effects.Base;

namespace HsLib.Cards.Abilities
{
    public class Fireblast : Ability
    {

        public Fireblast() : base(2)
        {
            DealDamageEffect effect = new() { Damage = 1 };
            Targets targets = new() { Locs = Loc.Field | Loc.Hero, Sides = PidSide.Me | PidSide.He };
            Effect = new TargetEffect(this, effect, EffectType.Solo, targets);
        }

        protected override ITargetEffect Effect { get; }
    }
}