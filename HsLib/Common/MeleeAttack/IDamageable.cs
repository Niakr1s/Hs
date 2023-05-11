using HsLib.Battle;
using HsLib.Common.Place;

namespace HsLib.Common.MeleeAttack
{
    public interface IDamageable : IWithPlace
    {
        bool CanBeMeleeAttacked(Battlefield bf);

        /// <summary>
        /// Gets some damage.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Amount of damage got</returns>
        int GetDamage(int value);
    }
}