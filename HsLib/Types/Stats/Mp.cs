namespace HsLib.Types.Stats
{
    public class Mp : IntStat
    {
        public Mp(int value) : base(value)
        {
        }

        public bool IsEnough(Mp mp)
        {
            return Value >= mp;
        }

        /// <summary>
        /// Uses mana. Throws exception if not sufficient mana.
        /// </summary>
        /// <param name="mp"></param>
        /// <exception cref="ValidationException"></exception>
        public void Use(Mp mp)
        {
            if (!IsEnough(mp)) { throw new ValidationException("not enough mp"); }
            Set(Value - mp);
        }
    }
}
