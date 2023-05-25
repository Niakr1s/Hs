using HsLib.Types.Cards;
using HsLib.Types.Choosers;
using HsLib.Types.Effects;
using HsLib.Types.Places;

namespace HsLib.KnownCards.Spells
{
    public class MindControl : Spell
    {
        public MindControl() : base(10)
        {
            SpellEffect = new(this,
                new MindControlEffect(),
                possibleTargetsChooser: new Targets { Locs = Loc.Field, Sides = PidSide.He }
                );
        }

        public override SpellEffect SpellEffect { get; }
    }
}
