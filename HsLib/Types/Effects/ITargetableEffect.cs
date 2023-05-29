
using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLib.Interfaces
{
    public interface ITargetableEffect
    {
        /// <summary>
        /// Owner of effect.
        /// </summary>
        ICard Owner { get; set; }

        /// <summary>
        /// Gets all targets from board, effect can be used on.
        /// </summary>
        /// <param name="board"></param>
        IEnumerable<ICard> GetPossibleTargets(IBoard board);

        /// <summary>
        /// Uses effect. 
        /// </summary>
        /// <param name="board"></param>
        /// <param name="target"></param>
        /// <exception cref="ValidationException">Any exception, that prevents for using effect</exception>
        /// <returns>Action, that actually uses effect</returns>
        Action UseEffect(IBoard board, ICard? target);

        /// <summary>
        /// Validates of validity or effect target while playing from hand.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="effectTarget"></param>
        /// <exception cref="ValidationException"></exception>
        /// 
        void ValidateEffectTarget(IBoard board, ICard? effectTarget);
    }
}
