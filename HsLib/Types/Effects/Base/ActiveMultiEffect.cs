using HsLib.Interfaces;

namespace HsLib.Types.Effects.Base
{
    public class ActiveMultiEffect : ActiveEffect
    {
        public ActiveMultiEffect(IEffect effect, ICardsChooser possibleTargetsChooser) :
            base(effect, possibleTargetsChooser: possibleTargetsChooser)
        {
        }
    }
}
