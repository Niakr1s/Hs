namespace HsLib.Types
{
    [Flags]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1135:Declare enum member with zero value (when enum has FlagsAttribute).",
        Justification = "<Pending>")] // never should be none
    public enum Loc
    {
        Deck = 1,
        Hand = 2,
        Field = 4,

        Hero = 8,
        Weapon = 16,
        Ability = 32,

        Secret = 64,

        Graveyard = 128,
    }

    public class LocException : ArgumentException
    {
        public LocException() : base()
        {
        }

        public LocException(string? message) : base(message)
        {
        }

        public LocException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
