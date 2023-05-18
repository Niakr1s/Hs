using HsLib.Interfaces;
using HsLib.Systems;

namespace HsLib.Types.Effects.Base
{
    public static class ActiveEffectValidator
    {
        /// <summary>
        /// Validates nullable active effect.
        /// </summary>
        /// <typeparam name="TOwner"></typeparam>
        /// <param name="activeEffect"></param>
        /// <param name="bf"></param>
        /// <param name="owner"></param>
        /// <param name="effectTarget"></param>
        /// <exception cref="ValidationException"></exception>
        public static void ValidateEffectTarget<TOwner>(IActiveEffect<TOwner>? activeEffect,
            Battlefield bf, TOwner owner, ICard? effectTarget)
        {
            if (activeEffect is null)
            {
                if (effectTarget is not null)
                {
                    throw new ValidationException("effect target should be null when active effect is null");
                }
            }
            else
            {
                activeEffect.ValidateEffectTarget(bf, owner, effectTarget);
            }
        }
    }
}
