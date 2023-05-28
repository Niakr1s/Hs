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
        /// <param name="bf"></param>
        /// <param name="target"></param>
        /// 
        /// <returns>Action, that actually uses effect.</returns>
        public Action UseAbility(Battlefield bf, ICard? target)
        {
            TargetableEffectValidator.ValidateEffectTarget(AbilityEffect, bf, target);
            Action useEffectAction = AbilityEffect.UseEffect(bf, target);

            return () =>
            {
                useEffectAction();
                EffectUsedThisTurn = true;
                bf[Place.Pid].Mp.Decrease(Mp);
            };
        }

        protected override void OnTurnStart()
        {
            base.OnTurnStart();
            EffectUsedThisTurn = false;
        }
    }
}
