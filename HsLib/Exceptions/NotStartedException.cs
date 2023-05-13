namespace HsLib.Exceptions
{
    public class NotStartedException : Exception
    {
        public NotStartedException() : base()
        {
        }

        public NotStartedException(string? message) : base(message)
        {
        }

        public NotStartedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
