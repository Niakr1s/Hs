using HsLib.Types.CardsChoosers;
using HsLib.Types.Places;

namespace HsLib.Types.Effects
{
    public class BattlecryEffect : PlayerEffect
    {
        public BattlecryEffect(IEffect effect,
            ICardsChooser<Pid>? possibleTargetsChooser = null, ICardsChooser<Pid>? targetsChooser = null)
            : base(effect, possibleTargetsChooser, targetsChooser)
        {
        }
    }
}