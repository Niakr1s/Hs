using HsLib.Interfaces;

namespace HsLib.Types
{
    public readonly struct Target
    {
        public HashSet<Loc> Locs { get; init; }

        public HashSet<PidSide> Sides { get; init; }

        public bool IsValidTarget(IWithPlace owner, IWithPlace? target)
        {
            if (owner.Place is null || target.Place is null) { return false; }

            if (target is null)
            {
                return Locs.Count == 0;
            }
            else
            {
                if (target.Place.Pid == Pid.None || target.Place.Loc == Loc.None) return false;

                bool sideIsCorrect = Sides.Contains(owner.Place.Pid.Side(target.Place.Pid));
                bool placeIsCorrect = Locs.Contains(target.Place.Loc);
                return sideIsCorrect && placeIsCorrect;
            }
        }

        public IEnumerable<TWithPlace> GetValidTargets<TWithPlace>(IWithPlace owner, IEnumerable<TWithPlace> targets)
            where TWithPlace : IWithPlace
        {
            foreach (var t in targets)
            {
                if (IsValidTarget(owner, t))
                {
                    yield return t;
                }
            }
        }
    }
}
