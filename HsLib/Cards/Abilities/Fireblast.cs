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
            Targets possibleTargets = new() { Locs = Loc.Field | Loc.Hero, Sides = PidSide.Me | PidSide.He };
            AbilityEffect = new ActiveEffect(effect, possibleTargetsChooser: possibleTargets);
        }

        public override IActiveEffect AbilityEffect { get; }
    }
}