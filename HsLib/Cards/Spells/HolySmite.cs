using HsLib.Interfaces;
using HsLib.Types;
using HsLib.Types.Cards;
using HsLib.Types.Effects;
using HsLib.Types.Effects.Base;

namespace HsLib.Cards.Spells
{
    public class HolySmite : Spell
    {
        public HolySmite() : base(1)
        {
            IEffect effect = new DealDamageEffect() { Damage = 2 };
            Targets possibleTargets = new Targets { Locs = Loc.Field | Loc.Hero, Sides = PidSide.He | PidSide.Me };
            SpellEffect = new ActiveEffect(effect, possibleTargetsChooser: possibleTargets);
        }
        public override IActiveEffect SpellEffect { get; }
    }
}
