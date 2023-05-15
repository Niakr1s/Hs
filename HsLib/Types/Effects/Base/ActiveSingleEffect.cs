using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLib.Types.Effects.Base
{
    public class ActiveSingleEffect : ActiveEffect
    {
        public ActiveSingleEffect(IEffect effect, ICardsChooser possibleTargetsChooser) :
            base(effect, possibleTargetsChooser)
        {
        }

        public override Action UseEffect(Battlefield bf, Pid pid, ICard? target)
        {
            Action? useEffectAction = target is null ? null : _effect.UseEffect(bf, target);

            return () => useEffectAction?.Invoke();
        }
    }
}
