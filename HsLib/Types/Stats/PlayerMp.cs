namespace HsLib.Types.Stats
{
    public class PlayerMp : IntStat
    {
        public PlayerMp(int value) : base(value)
        {
        }

        public bool IsEnough(int value)
        {
            return Value >= value;
        }

        /// <summary>
        /// Uses mana. Throws exception if not sufficient mana.
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="ArgumentException"></exception>
        public override void Decrease(int value)
        {
            if (!IsEnough(value)) { throw new ArgumentException("not enough mp"); }
            base.Decrease(value);
        }
    }
}
