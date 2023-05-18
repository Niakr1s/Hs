namespace HsLib.Types.Turns
{
    public class TurnNotStartedException : Exception
    {
        public TurnNotStartedException() : base()
        {
        }

        public TurnNotStartedException(string? message) : base(message)
        {
        }

        public TurnNotStartedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
