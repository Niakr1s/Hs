using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLib.Types.Effects.Base
{
    public class MassTargetEffect : TargetEffect
    {
        public MassTargetEffect(IEffect effect, ICardsChooser possibleCardsChooser) :
            base(effect, possibleCardsChooser)
        {
        }

        public override void UseEffect(Battlefield bf, Pid pid, ICard? target)
        {
            foreach (ICard card in GetPossibleTargets(bf, pid))
            {
                _effect.UseEffect(bf, card);
            }
        }
    }
}
