namespace HsLib.Types.Places
{
    public record Place(Pid Pid, Loc Loc)
    {
        public Place() : this(Pid.None, Loc.None) { }

        public bool IsNone()
        {
            return Pid == Pid.None || Loc == Loc.None;
        }
    }
}
