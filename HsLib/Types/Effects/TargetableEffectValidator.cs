using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLib.Types.Effects
{
    public static class TargetableEffectValidator
    {
        /// <summary>
        /// Validates nullable active effect.
        /// </summary>
        /// <param name="activeEffect"></param>
        /// <param name="board"></param>
        /// <param name="effectTarget"></param>
        /// <exception cref="ValidationException"></exception>
        public static void ValidateEffectTarget(ITargetableEffect? activeEffect,
            IBoard board, ICard? effectTarget)
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
                activeEffect.ValidateEffectTarget(board, effectTarget);
            }
        }

        public static bool IsEffectTargetValid(ITargetableEffect? activeEffect,
            IBoard board, ICard? effectTarget)
        {
            try
            {
                ValidateEffectTarget(activeEffect, board, effectTarget);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
