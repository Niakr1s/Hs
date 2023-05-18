
using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLib.Interfaces
{
    public interface IActiveEffect<TOwner>
    {
        /// <summary>
        /// Gets all targets from battlefield, effect can be used on.
        /// </summary>
        /// <param name="bf"></param>
        /// <param name="owner"></param>
        IEnumerable<ICard> GetPossibleTargets(Battlefield bf, TOwner owner);

        /// <summary>
        /// Uses effect. 
        /// </summary>
        /// <param name="bf"></param>
        /// <param name="owner"></param>
        /// <param name="target"></param>
        /// <exception cref="ValidationException">Any exception, that prevents for using effect</exception>
        /// <returns>Action, that actually uses effect</returns>
        Action UseEffect(Battlefield bf, TOwner owner, ICard? target);

        /// <summary>
        /// Validates of validity or effect target while playing from hand.
        /// </summary>
        /// <param name="bf"></param>
        /// <param name="owner"></param>
        /// <param name="effectTarget"></param>
        /// <exception cref="ValidationException"></exception>
        /// 
        void ValidateEffectTarget(Battlefield bf, TOwner owner, ICard? effectTarget);
    }
}
