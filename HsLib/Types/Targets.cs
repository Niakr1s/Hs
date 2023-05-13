using HsLib.Interfaces;

namespace HsLib.Types
{
    public readonly struct Targets
    {
        public Loc? Locs { get; init; }

        public PidSide? Sides { get; init; }

        public bool IsValidTarget(IWithPlace owner, IWithPlace? target)
        {
            if (owner.Place is null) { return false; }

            if (target is null)
            {
                return Locs is null;
            }
            else
            {
                if (target.Place is null) { return false; }

                bool sideIsCorrect = Sides?.HasFlag(owner.Place.Pid.Side(target.Place.Pid)) == true;
                bool locIsCorrect = Locs?.HasFlag(target.Place.Loc) == true;
                return sideIsCorrect && locIsCorrect;
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
