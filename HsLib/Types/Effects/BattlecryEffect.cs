using HsLib.Types.Choosers;
using HsLib.Types.Places;

namespace HsLib.Types.Effects
{
    public class BattlecryEffect : PlayerEffect
    {
        public BattlecryEffect(IEffect effect,
            IChooser<Pid>? possibleTargetsChooser = null, IChooser<Pid>? targetsChooser = null)
            : base(effect, possibleTargetsChooser, targetsChooser)
        {
        }
    }
}