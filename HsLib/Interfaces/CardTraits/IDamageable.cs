using HsLib.Systems;

namespace HsLib.Interfaces.CardTraits
{
    public interface IDamageable : ICard
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