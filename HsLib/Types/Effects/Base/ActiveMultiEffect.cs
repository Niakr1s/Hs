using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLib.Types.Effects.Base
{
    public class ActiveMultiEffect : ActiveEffect
    {
        public ActiveMultiEffect(IEffect effect, ICardsChooser possibleCardsChooser) :
            base(effect, possibleCardsChooser)
        {
        }

        public override Action UseEffect(Battlefield bf, Pid pid, ICard? target)
        {
            List<Action> actions = GetPossibleTargets(bf, pid).Select(card => _effect.UseEffect(bf, card)).ToList();
            return () => actions.ForEach(a => a());
        }
    }
}
