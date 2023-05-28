using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Places;

namespace HsLib.Types.Choosers
{
    public readonly struct Targets : IChooser
    {
        public Loc Locs { get; init; }

        public PidSide Sides { get; init; }

        private bool IsValidTarget(ICard owner, ICard target)
        {
            if (target.Place.IsNone()) { return false; }

            bool sideIsCorrect = Sides.HasFlag(owner.Place.Pid.Side(target.Place.Pid));
            bool locIsCorrect = Locs.HasFlag(target.Place.Loc);
            return sideIsCorrect && locIsCorrect;
        }

        public IEnumerable<ICard> ChooseCards(Battlefield bf, ICard owner, IEnumerable<ICard> cards)
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
