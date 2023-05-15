using HsLib.Types.Effects.Base;

namespace HsLib.Types.Effects
{
    public class MindControlTargetEffect : SingleTargetEffect
    {
        public MindControlTargetEffect() :
            base(new MindControlEffect(), new Targets { Locs = Loc.Field, Sides = PidSide.He })
        {
        }
    }
}