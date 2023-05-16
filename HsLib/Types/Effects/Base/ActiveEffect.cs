﻿
using HsLib.Interfaces;
using HsLib.Systems;

namespace HsLib.Types.Effects.Base
{
    public class ActiveEffect : IActiveEffect
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="effect"></param>
        /// <param name="possibleTargetsChooser">Chooses possible targets for effect.</param>
        /// <param name="targetsChooser">Chooses targets for effect.
        /// If provided, will ignore target in <see cref="UseEffect(Battlefield, Pid, ICard?)"/> and use it instead.</param>
        public ActiveEffect(IEffect effect, ICardsChooser? possibleTargetsChooser = null, ICardsChooser? targetsChooser = null)
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
        /// Uses effect. Param target can be ignored if custom targets chooser is provided.
        /// </summary>
        /// <param name="bf"></param>
        /// <param name="pid"></param>
        /// <param name="target">Will be ignored, if <see cref="_targetsChooser"/>is set, and will uses effects on every target, choosed by it.</param>
        public Action UseEffect(Battlefield bf, Pid pid, ICard? target)
        {
            List<Action> effectActions = new();
            if (_targetsChooser is null)
            {
                if (target is not null)
                {
                    effectActions.Add(_effect.UseEffect(bf, target));
                }
            }
            else
            {
                if (target is not null) { throw new ValidationException("target should be null"); }

                IEnumerable<Action>? toAdd = _targetsChooser?.ChooseCards(pid, bf.Cards).Select(target => _effect.UseEffect(bf, target));
                if (toAdd is not null) effectActions.AddRange(toAdd);
            }
            return () => effectActions?.ForEach(a => a());
        }
    }
}
