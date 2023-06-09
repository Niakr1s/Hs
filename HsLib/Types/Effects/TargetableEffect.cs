﻿using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Choosers;

namespace HsLib.Types.Effects
{
    public abstract class TargetableEffect : ITargetableEffect
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="effect"></param>
        /// <param name="possibleTargetsChooser">Chooses possible targets for effect.</param>
        /// <param name="targetsChooser">Chooses targets for effect.
        /// If provided, will ignore target in <see cref="UseEffect(Board, PlaceInContainer, ICard?)"/> and use it instead.</param>
        public TargetableEffect(ICard owner, IEffect effect,
            IChooser<ICard>? possibleTargetsChooser = null,
            IChooser<ICard>? targetsChooser = null)
        {
            Owner = owner;
            Effect = effect;
            _targetsChooser = targetsChooser;
            _possibleTargetsChooser = possibleTargetsChooser;
        }
        private readonly IChooser<ICard>? _targetsChooser;
        private readonly IChooser<ICard>? _possibleTargetsChooser;

        public ICard Owner { get; set; }

        public IEffect Effect { get; }

        public IEnumerable<ICard> GetPossibleTargets(IBoard board)
        {
            if (_possibleTargetsChooser is null) { yield break; }

            foreach (var t in _possibleTargetsChooser.ChooseCards(board, Owner))
            {
                yield return t;
            }
        }

        /// <summary>
        /// Uses effect. Param target can be ignored if custom targets chooser is provided.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="target">Will be ignored, if <see cref="_targetsChooser"/>is set, and will uses effects on every target, choosed by it.</param>
        /// 
        public Action UseEffect(IBoard board, ICard? target)
        {
            List<Action> effectActions = new();
            if (_targetsChooser is null)
            {
                effectActions.Add(Effect.UseEffect(board, Owner, target));
            }
            else
            {
                if (target is not null) { throw new ValidationException("target should be null"); }

                IEnumerable<Action>? toAdd = _targetsChooser?.ChooseCards(board, Owner)
                    .Select(target => Effect.UseEffect(board, Owner, target));

                if (toAdd is not null) effectActions.AddRange(toAdd);
            }
            return () => effectActions?.ForEach(a => a());
        }

        public abstract void ValidateEffectTarget(IBoard board, ICard? effectTarget);
    }
}
