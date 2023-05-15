using HsLib.Types.Effects.Base;

namespace HsLib.Types.Effects
{
    public class MindControlTargetEffect : ActiveEffect
    {
        public MindControlTargetEffect() :
            base(new MindControlEffect(), possibleTargetsChooser: new Targets { Locs = Loc.Field, Sides = PidSide.He })
        {
        }
    }
}