using HsLib.Interfaces;

namespace HsLib.Types.Effects.Base
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