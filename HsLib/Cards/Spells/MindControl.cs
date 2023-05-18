using HsLib.Types;
using HsLib.Types.Cards;
using HsLib.Types.Effects;
using HsLib.Types.Effects.Base;

namespace HsLib.Cards.Spells
{
    public class MindControl : Spell
    {
        public MindControl() : base(10)
        {
            SpellEffect.Effect = new ActiveEffect<Pid>(
                new MindControlEffect(),
                possibleTargetsChooser: new Targets { Locs = Loc.Field, Sides = PidSide.He }
                );
        }
    }
}
