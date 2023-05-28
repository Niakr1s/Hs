using HsLib.Systems;
using HsLib.Types.Effects;

namespace HsLib.Types.Cards
{
    public abstract class Ability : Card
    {
        protected Ability(int mp) : base(mp)
        {
        }

        public abstract AbilityEffect AbilityEffect { get; }

        public bool EffectUsedThisTurn { get; private set; }

        /// <summary>
        /// First call will throw exceptions, if effect can't be used. Second call actually uses effect.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="target"></param>
        /// 
        /// <returns>Action, that actually uses effect.</returns>
        public Action UseAbility(Board board, ICard? target)
        {
            TargetableEffectValidator.ValidateEffectTarget(AbilityEffect, board, target);
            Action useEffectAction = AbilityEffect.UseEffect(board, target);

            return () =>
            {
                useEffectAction();
                EffectUsedThisTurn = true;
                board[Place.Pid].Mp.Decrease(Mp);
            };
        }

        protected override void OnTurnStart()
        {
            base.OnTurnStart();
            EffectUsedThisTurn = false;
        }
    }
}
