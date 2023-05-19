namespace HsLib.Types.Stats
{
    public class Hp : IntStat
    {
        public Hp(int value) : base(value)
        {
        }

        public event EventHandler<HpGotDamageEventArgs>? GotDamage;

        /// <summary>
        /// Gets amount of damage.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Amount of damage received</returns>
        public int GetDamage(int value)
        {
            Decrease(value);
            GotDamage?.Invoke(this, new HpGotDamageEventArgs(value));
            return value;
        }
    }
}
