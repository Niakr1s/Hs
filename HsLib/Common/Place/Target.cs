namespace HsLib.Common.Place
{
    public readonly struct Target
    {
        public HashSet<Loc> Locs { get; init; }

        public HashSet<PidSide> Sides { get; init; }

        public bool IsValidTarget(IWithPlace owner, IWithPlace? target)
        {
            if (owner.Pid == Pid.None || owner.Loc == Loc.None) return false;

            if (target is null)
            {
                return Locs.Count == 0;
            }
            else
            {
                if (target.Pid == Pid.None || target.Loc == Loc.None) return false;

                bool sideIsCorrect = Sides.Contains(owner.Pid.Side(target.Pid));
                bool placeIsCorrect = Locs.Contains(target.Loc);
                return sideIsCorrect && placeIsCorrect;
            }
        }
    }
}
