using Models.Common;

namespace Models.Cards.Effects
{
    public readonly struct EffectTarget
    {
        public EffectTargetPlace Place { get; init; }

        public EffectTargetSide Side { get; init; }

        public bool IsValidTarget(Card owner, Card? target)
        {
            if (owner.Pid == Pid.None || owner.Loc == Loc.None) return false;

            if (target is null)
            {
                return Place == EffectTargetPlace.None;
            }
            else
            {
                if (target.Pid == Pid.None || target.Loc == Loc.None) return false;

                bool sameSide = owner.Pid == target.Pid;
                bool sideIsCorrect =
                    (Side.HasFlag(EffectTargetSide.Me) && sameSide) ||
                    (Side.HasFlag(EffectTargetSide.He) && !sameSide);

                bool placeIsCorrect =
                    (Place.HasFlag(EffectTargetPlace.Field) && target.Loc == Loc.Field) ||
                    (Place.HasFlag(EffectTargetPlace.Hero) && target.Loc == Loc.Hero);

                return sideIsCorrect && placeIsCorrect;
            }
        }
    }

    [Flags]
    public enum EffectTargetPlace
    {
        None = 0,
        Field = 1,
        Hero = 2,
    }

    [Flags]
    public enum EffectTargetSide
    {
        None = 0,
        Me = 1,
        He = 2,
    }
}
