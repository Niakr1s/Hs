using Models.Common;

namespace Models.Services.Battle
{
    public interface IDefender
    {
        public Pid Pid { get; }

        public Loc Loc { get; }

        /// <summary>
        /// Gets some damage.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Amount of damage got</returns>
        public int GetDamage(int value);
    }
}