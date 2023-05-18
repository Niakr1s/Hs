using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLib.Types.Effects
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