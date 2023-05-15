using HsLib.Interfaces;

namespace HsLib.Types.Effects.Base
{
    public class ActiveSingleEffect : ActiveEffect
    {
        public ActiveSingleEffect(IEffect effect, ICardsChooser possibleTargetsChooser) :
            base(effect, possibleTargetsChooser: possibleTargetsChooser)
        {
        }
    }
}
