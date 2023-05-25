using HsLib.Types.Cards;
using HsLib.Types.Choosers;
using HsLib.Types.Effects;
using HsLib.Types.Places;

namespace HsLib.KnownCards.Spells
{
    public class HolySmite : Spell
    {
        public HolySmite() : base(1)
        {
            IEffect effect = new DealDamageEffect() { Damage = 2 };
            Targets possibleTargets = new Targets { Locs = Loc.Field | Loc.Hero, Sides = PidSide.He | PidSide.Me };
            SpellEffect = new(this, effect, possibleTargetsChooser: possibleTargets);
        }

        public override SpellEffect SpellEffect { get; }
    }
}
