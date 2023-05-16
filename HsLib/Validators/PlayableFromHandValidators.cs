using HsLib.Exceptions;
using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types;

namespace HsLib.Validators
{
    internal static class PlayableFromHandValidators
    {
        /// <summary>
        /// Validates effect target.
        /// </summary>
        /// <param name="bf"></param>
        /// <param name="pid"></param>
        /// <param name="effectTarget"></param>
        /// <param name="playFromHandEffect"></param>
        /// <exception cref="ValidationException"></exception>
        public static void ValidateEffectTarget(Battlefield bf, Pid pid, ICard? effectTarget, IActiveEffect? playFromHandEffect)
        {
            // todo: make tests
            if (playFromHandEffect is not null)
            {
                if (effectTarget is null)
                {
                    if (playFromHandEffect.GetPossibleTargets(bf, pid).Any())
                    {
                        throw new ValidationException("effect target is null even though possible targets are present");
                    }
                }
                else
                {
                    if (!playFromHandEffect.GetPossibleTargets(bf, pid).Contains(effectTarget))
                    {
                        throw new ValidationException(
                        "effect target is not null even though no possible targets are present");
                    }
                }
            }
            else
            {
                if (effectTarget is not null)
                {
                    throw new ValidationException(
                    "effect target is not null even though battlecry is null");
                }
            }
        }
    }
}