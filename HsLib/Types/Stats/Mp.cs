using HsLib.Types.Stats.Base;

namespace HsLib.Types.Stats
{
    public class Mp : IntStat
    {
        public Mp(int value) : base(value)
        {
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
