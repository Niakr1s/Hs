using HsLib.Systems;
using HsLib.Types;
using HsLib.Types.Cards;

namespace HsLib.Interfaces
{
    public interface ITargetEffect
    {
        /// <summary>
        /// Gets all targets from battlefield, effect can be used on.
        /// </summary>
        /// <param name="bf"></param>
        /// <param name="pid">owner's pid</param>
        IEnumerable<ICard> UseEffectTargets(Battlefield bf, Pid pid);

        void UseEffect(Battlefield bf, Pid pid, ICard? target);
    }
}
