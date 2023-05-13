using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Effects;

namespace HsLib.Interfaces
{
    public interface IEffect
    {
        public EffectType EffectType { get; }

        /// <summary>
        /// Gets all targets from battlefield, effect can be used on.
        /// </summary>
        /// <param name="bf"></param>
        /// <returns>All targets. If returned null, IEffect can not be applied. If effect can be applied without target, just return</returns>
        public IEnumerable<Card> UseEffectTargets(Battlefield bf);

        /// <summary>
        /// Uses effect on target.
        /// </summary>
        /// <param name="bf"></param>
        /// <param name="target"></param>
        public void UseEffect(Battlefield bf, Card? target);
    }
}
