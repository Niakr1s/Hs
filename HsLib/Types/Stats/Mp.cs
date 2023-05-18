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
        /// <exception cref="MpException"></exception>
        public void Use(Mp mp)
        {
            if (!IsEnough(mp)) { throw new MpException("not enough mp"); }
            Set(Value - mp);
        }
    }

    public class MpException : Exception
    {
        public MpException() : base()
        {
        }

        public MpException(string? message) : base(message)
        {
        }

        public MpException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
