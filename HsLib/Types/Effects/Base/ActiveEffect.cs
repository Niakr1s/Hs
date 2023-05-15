using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLib.Types.Effects.Base
{
    public class ActiveEffect : IActiveEffect
    {
        public ActiveEffect(IEffect effect, ICardsChooser? targetsChooser = null, ICardsChooser? possibleTargetsChooser = null)
        {
            _effect = effect;
            _targetsChooser = targetsChooser;
            _possibleTargetsChooser = possibleTargetsChooser ?? new Targets();
        }

        protected readonly IEffect _effect;
        private readonly ICardsChooser? _targetsChooser;
        private readonly ICardsChooser _possibleTargetsChooser;

        public IEnumerable<ICard> GetPossibleTargets(Battlefield bf, Pid pid) => _possibleTargetsChooser.ChooseCards(pid, bf.Cards);

        /// <summary>
        /// Вместо параметра target будет вызываться кастомный targetChooser.
        /// </summary>
        /// <param name="bf"></param>
        /// <param name="pid"></param>
        /// <param name="target"></param>
        public Action UseEffect(Battlefield bf, Pid pid, ICard? target)
        {
            List<Action> effectActions = new();
            if (_targetsChooser is null)
            {
                if (target is null)
                {
                    effectActions.AddRange(_possibleTargetsChooser.ChooseCards(pid, bf.Cards).Select(target => _effect.UseEffect(bf, target)));
                }
                else
                {
                    effectActions.Add(_effect.UseEffect(bf, target));
                }
            }
            else
            {
                if (target is not null) { throw new ArgumentException("target should be null"); }

                IEnumerable<Action>? toAdd = _targetsChooser?.ChooseCards(pid, bf.Cards).Select(target => _effect.UseEffect(bf, target));
                if (toAdd is not null) effectActions.AddRange(toAdd);
            }
            return () => effectActions?.ForEach(a => a());
        }
    }
}
