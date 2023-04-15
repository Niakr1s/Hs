using HsLib.Stats.Base;

namespace HsLib.Stats
{
    public class Hp : IntStat
    {
        public Hp(int value) : base(value)
        {
        }

        public void Silence()
        {
            int hpWithEnchants = Value;
            Auras.Clear();
            Buffs.Clear();
            _value = hpWithEnchants > _initialValue ? _initialValue : hpWithEnchants;
        }

        /// <summary>
        /// Gets amount of damage.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Amount of damage received</returns>
        public int GetDamage(int value)
        {
            _value -= value;
            return value;
        }
    }
}
