using HsLib.Types.Cards;
using HsLib.Types.Choosers;

namespace HsLib.Types.Effects
{
    public class BattlecryEffect : PlayerEffect
    {
        public BattlecryEffect(ICard owner, IEffect effect,
            IChooser? possibleTargetsChooser = null, IChooser? targetsChooser = null)
            : base(owner, effect, possibleTargetsChooser, targetsChooser)
        {
        }
    }
}