using HsLib.Common.Place;

namespace HsLib.Battle
{
    public interface IDamageable : IWithPlace
    {
        public bool Dead { get; }

        /// <summary>
        /// Gets some damage.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Amount of damage got</returns>
        public int GetDamage(int value);
    }
}