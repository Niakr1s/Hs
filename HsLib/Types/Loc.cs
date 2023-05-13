namespace HsLib.Types
{
    public enum Loc
    {
        None,

        Deck,
        Hand,
        Field,

        Hero,
        Weapon,
        Ability,

        Secret,

        Graveyard,
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
