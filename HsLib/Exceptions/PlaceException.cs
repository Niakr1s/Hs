namespace HsLib.Exceptions
{
    internal class PlaceException : Exception
    {
        public PlaceException() : base()
        {
        }

        public PlaceException(string? message) : base(message)
        {
        }

        public PlaceException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
