using HsLib.Systems;
using HsLib.Types;
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
        /// <param name="pid">owner's pid</param>
        /// <returns>All targets. If returned null, IEffect can not be applied. If effect can be applied without target, just return</returns>
        public IEnumerable<Card> UseEffectTargets(Battlefield bf, Pid pid);

        /// <summary>
        /// Uses effect on target.
        /// </summary>
        /// <param name="bf"></param>
        /// <param name="pid">owner's pid</param>
        /// <param name="target"></param>
        public void UseEffect(Battlefield bf, Pid pid, Card? target);
    }
}
