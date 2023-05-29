using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLib.Types.Effects
{
    public interface IEffect
    {
        /// <summary>
        /// Uses effect on target.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="owner">
        /// There are many effects, that doen't need owner param, but
        /// here are some effects, that need it (for example transform effect etc),
        /// </param>
        /// <param name="target"></param>
        /// <exception cref="ValidationException"></exception>
        /// <returns>Action, that actually uses effect</returns>
        Action UseEffect(IBoard board, ICard owner, ICard? target);
    }
}