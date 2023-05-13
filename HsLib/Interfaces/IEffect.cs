using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLib.Interfaces
{
    public interface IEffect
    {
        /// <summary>
        /// Gets all targets from battlefield, effect can be used on.
        /// </summary>
        /// <param name="bf"></param>
        /// <param name="owner"></param>
        /// <returns>All targets. If returned null, IEffect can not be applied. If effect can be applied without target, just return</returns>
        public IEnumerable<Card> UseEffectTargets(Battlefield bf);

        public bool EffectMustHaveTarget { get; }

        /// <summary>
        /// Shows if effect can be used physically.
        /// <br/>Examples: Ability should return false, if it was already played;
        /// MindControl spell should return false, if it's side's field is full.
        /// </summary>
        /// <param name="bf"></param>
        /// <returns></returns>
        public bool CanUseEffect(Battlefield bf);

        /// <summary>
        /// Uses effect on target.
        /// </summary>
        /// <param name="bf"></param>
        /// <param name="target"></param>
        public void UseEffect(Battlefield bf, Card? target);
    }
}
