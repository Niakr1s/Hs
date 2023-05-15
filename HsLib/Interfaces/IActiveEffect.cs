using HsLib.Systems;
using HsLib.Types;

namespace HsLib.Interfaces
{
    public interface IActiveEffect
    {
        /// <summary>
        /// Gets all targets from battlefield, effect can be used on.
        /// </summary>
        /// <param name="bf"></param>
        /// <param name="pid">owner's pid</param>
        IEnumerable<ICard> GetPossibleTargets(Battlefield bf, Pid pid);

        /// <summary>
        /// Uses effect. 
        /// </summary>
        /// <param name="bf"></param>
        /// <param name="pid"></param>
        /// <param name="target"></param>
        /// <exception cref="Exception">Any exception, that prevents for using effect</exception>
        /// <returns>Action, that actually uses effect</returns>
        Action UseEffect(Battlefield bf, Pid pid, ICard? target);
    }
}
