using HsLib.Types.Cards;
using HsLib.Types.Places;

namespace HsLib.Types.Choosers
{
    public readonly struct Targets : IChooser<PlaceInContainer>, IChooser<Pid>
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
                if (target.PlaceInContainer is null) { return false; }

                bool sideIsCorrect = Sides?.HasFlag(ownerPid.Side(target.PlaceInContainer.Pid)) == true;
                bool locIsCorrect = Locs?.HasFlag(target.PlaceInContainer.Loc) == true;
                return sideIsCorrect && locIsCorrect;
            }
        }

        public IEnumerable<ICard> ChooseCards(PlaceInContainer ownerPlaceInContainer, IEnumerable<ICard> cards)
        {
            foreach (var t in ChooseCards(ownerPlaceInContainer.Pid, cards))
            {
                yield return t;
            }
        }

        public IEnumerable<ICard> ChooseCards(Pid owner, IEnumerable<ICard> cards)
        {
            foreach (var t in cards)
            {
                if (IsValidTarget(owner, t))
                {
                    yield return t;
                }
            }
        }
    }
}
