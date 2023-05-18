namespace HsLib.Types.Places
{
    public record Place(Pid Pid, Loc Loc)
    {
        public PlaceInContainer InContainer(int addedTurn, int atIndex)
        {
            return new PlaceInContainer(Pid, Loc, addedTurn, atIndex);
        }
    }
}
