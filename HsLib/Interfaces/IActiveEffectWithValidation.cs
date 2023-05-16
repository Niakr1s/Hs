using HsLib.Systems;
using HsLib.Types;

namespace HsLib.Interfaces
{
    public interface IActiveEffectWithValidation : IActiveEffect
    {
        /// <summary>
        /// Validates of validity or effect target while playing from hand.
        /// </summary>
        /// <param name="bf"></param>
        /// <param name="pid"></param>
        /// <param name="effectTarget"></param>
        /// <exception cref="ValidationException"></exception>
        void ValidateEffectTarget(Battlefield bf, Pid pid, ICard? effectTarget);
    }
}
