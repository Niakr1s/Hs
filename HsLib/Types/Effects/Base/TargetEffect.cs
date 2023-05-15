using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLib.Types.Effects.Base
{
    public abstract class TargetEffect : ITargetEffect
    {
        protected TargetEffect(IEffect effect, ICardsChooser possibleTargetsChooser)
        {
            _effect = effect;
            _possibleTargetsChooser = possibleTargetsChooser;
        }

        protected readonly IEffect _effect;
        private readonly ICardsChooser _possibleTargetsChooser;

        public IEnumerable<ICard> GetPossibleTargets(Battlefield bf, Pid pid) => _possibleTargetsChooser.ChooseCards(pid, bf.Cards);

        public abstract void UseEffect(Battlefield bf, Pid pid, ICard? target);
    }
}
