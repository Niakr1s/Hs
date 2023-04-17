using HsLib.Battle;

namespace HsLib.Cards.Effects
{
    public interface IEffect
    {
        /// <summary>
        /// Gets all targets from battlefield, effect can be used on.
        /// </summary>
        /// <param name="bf"></param>
        /// <param name="owner"></param>
        /// <returns>All targets</returns>
        public IEnumerable<Card> UseEffectTargets(Battlefield bf);

        /// <summary>
        /// Uses effect on target.
        /// </summary>
        /// <param name="bf"></param>
        /// <param name="target"></param>
        public void UseEffect(Battlefield bf, Card? target);
    }
}
