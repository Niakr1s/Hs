using HsLib.Systems;

namespace HsLib.Interfaces
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