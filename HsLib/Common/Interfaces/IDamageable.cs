using HsLib.Common.Place;

namespace HsLib.Common.Interfaces
{
    public interface IDamageable : IWithPlace
    {
        /// <summary>
        /// Gets some damage.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Amount of damage got</returns>
        public int GetDamage(int value);
    }
}