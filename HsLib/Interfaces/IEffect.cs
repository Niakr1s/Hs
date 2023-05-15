using HsLib.Exceptions;
using HsLib.Systems;

namespace HsLib.Interfaces
{
    public interface IEffect
    {
        /// <summary>
        /// Uses effect on target.
        /// </summary>
        /// <param name="bf"></param>
        /// <param name="target"></param>
        /// <exception cref="ValidationException"></exception>
        /// <returns>Action, that actually uses effect</returns>
        Action UseEffect(Battlefield bf, ICard target);
    }
}