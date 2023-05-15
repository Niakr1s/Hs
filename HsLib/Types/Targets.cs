using HsLib.Interfaces;

namespace HsLib.Types
{
    public readonly struct Targets : ICardsChooser
    {
        public Loc? Locs { get; init; }

        public PidSide? Sides { get; init; }

        private bool IsValidTarget(Pid ownerPid, ICard? target)
        {
            if (target is null)
            {
                return Locs is null;
            }
            else
            {
                if (target.Place is null) { return false; }

                bool sideIsCorrect = Sides?.HasFlag(ownerPid.Side(target.Place.Pid)) == true;
                bool locIsCorrect = Locs?.HasFlag(target.Place.Loc) == true;
                return sideIsCorrect && locIsCorrect;
            }
        }

        public IEnumerable<ICard> ChooseCards(Pid ownerPid, IEnumerable<ICard> targets)
        {
            foreach (var t in targets)
            {
                if (IsValidTarget(ownerPid, t))
                {
                    yield return t;
                }
            }
        }
    }
}
