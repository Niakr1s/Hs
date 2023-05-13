using HsLib.Interfaces;
using HsLib.Types;
using HsLib.Types.Cards;
using HsLib.Types.Effects;

namespace HsLib.Cards.Abilities
{
    public class Fireblast : Ability
    {

        public Fireblast() : base(2)
        {
            Effect = new DealDamageEffect(this, true, new Targets { Locs = Loc.Field | Loc.Hero, Sides = PidSide.Me | PidSide.He })
            {
                Damage = 1
            };
        }

        protected override IEffect Effect { get; }
    }
}