namespace HsLib.Types
{
    public record Place(Pid Pid, Loc Loc)
    {
        public PlaceInContainer InContainer(int atIndex)
        {
            return new PlaceInContainer(Pid, Loc, atIndex);
        }
    }

    public record PlaceInContainer(Pid Pid, Loc Loc, int Index)
    {
        public static implicit operator Place(PlaceInContainer placeInContainer)
        {
            return new Place(placeInContainer.Pid, placeInContainer.Loc);
        }
    }

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
