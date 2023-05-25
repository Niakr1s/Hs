using HsLib.Types.Cards;
using HsLib.Types.Choosers;
using HsLib.Types.Places;

namespace HsLib.Types.Effects
{
    public class BattlecryEffect : PlayerEffect
    {
        public BattlecryEffect(ICard owner, IEffect effect,
            IChooser<Pid>? possibleTargetsChooser = null, IChooser<Pid>? targetsChooser = null)
            : base(owner, effect, possibleTargetsChooser, targetsChooser)
        {
        }
    }
}