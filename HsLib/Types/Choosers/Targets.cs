using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Places;

namespace HsLib.Types.Choosers
{
    public readonly struct Targets : IChooser<ICard>, IChooser<Pid>
    {
        public Loc Locs { get; init; }

        public PidSide Sides { get; init; }

        private bool IsValidTarget(ICard owner, ICard target)
        {
            return IsValidTarget(owner.Place.Pid, target);
        }

        private bool IsValidTarget(Pid ownerPid, ICard target)
        {
            if (target.Place.IsNone()) { return false; }
            if (ownerPid == Pid.None) { return false; }

            bool sideIsCorrect = Sides.HasFlag(ownerPid.Side(target.Place.Pid));
            bool locIsCorrect = Locs.HasFlag(target.Place.Loc);
            return sideIsCorrect && locIsCorrect;
        }

        public IEnumerable<ICard> ChooseCards(Battlefield bf, ICard owner)
        {
            foreach (var t in bf.Cards)
            {
                if (IsValidTarget(owner, t))
                {
                    yield return t;
                }
            }
        }

        public IEnumerable<ICard> ChooseCards(Battlefield bf, Pid owner)
        {
            foreach (var t in bf.Cards)
            {
                if (IsValidTarget(owner, t))
                {
                    yield return t;
                }
            }
        }
    }
}
