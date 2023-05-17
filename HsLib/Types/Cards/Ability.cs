using HsLib.Interfaces;
using HsLib.Systems;

namespace HsLib.Types.Cards
{
    public abstract class Ability : Card
    {
        protected Ability(int mp) : base(mp)
        {
        }

        public abstract IActiveEffect<Pid> AbilityEffect { get; }

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
            Action useEffectAction = AbilityEffect.UseEffect(bf, PlaceInContainer!.Pid, target);

            return () =>
            {
                useEffectAction();
                EffectUsedThisTurn = true;
            };
        }

        public override void OnTurnStart(Battlefield bf)
        {
            base.OnTurnStart(bf);
            EffectUsedThisTurn = false;
        }
    }
}
