namespace HsLib.Types.Places
{
    [Flags]
    public enum Loc
    {
        None = 0,

        Deck = 1,
        Hand = 2,
        Field = 4,

        Hero = 8,
        Weapon = 16,
        Ability = 32,

        Secrets = 64,
    }

    public static class LocExtensions
    {
        private static readonly Loc _multi = Loc.Deck | Loc.Hand | Loc.Field;
        public static bool IsMulti(this Loc loc)
        {
            return _multi.HasFlag(loc);
        }

        private static readonly Loc _single = Loc.Deck | Loc.Hand | Loc.Field;
        public static bool IsSingle(this Loc loc)
        {
            return _single.HasFlag(loc);
        }
    }
}
