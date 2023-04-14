using Models.Common;

namespace Models.Cards.Effects
{
    public readonly struct Target
    {
        public TargetContainer Container { get; init; }

        public TargetSide Side { get; init; }

        public bool IsValidTarget(Card owner, Card? target)
        {
            if (owner.Pid == Pid.None || owner.Loc == Loc.None) return false;

            if (target is null)
            {
                return Container == TargetContainer.None;
            }
            else
            {
                if (target.Pid == Pid.None || target.Loc == Loc.None) return false;

                bool sameSide = owner.Pid == target.Pid;
                bool sideIsCorrect =
                    (Side.HasFlag(TargetSide.Me) && sameSide) ||
                    (Side.HasFlag(TargetSide.He) && !sameSide);

                bool placeIsCorrect =
                    (Container.HasFlag(TargetContainer.Field) && target.Loc == Loc.Field) ||
                    (Container.HasFlag(TargetContainer.Hero) && target.Loc == Loc.Hero);

                return sideIsCorrect && placeIsCorrect;
            }
        }
    }

    [Flags]
    public enum TargetContainer
    {
        None = 0,
        Field = 1,
        Hero = 2,
    }

    [Flags]
    public enum TargetSide
    {
        None = 0,
        Me = 1,
        He = 2,
    }
}
