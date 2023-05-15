using HsLib.Types.Effects.Base;

namespace HsLib.Types.Effects
{
    public class MindControlActiveEffect : ActiveEffect
    {
        public MindControlActiveEffect() :
            base(new MindControlEffect(), possibleTargetsChooser: new Targets { Locs = Loc.Field, Sides = PidSide.He })
        {
        }
    }
}