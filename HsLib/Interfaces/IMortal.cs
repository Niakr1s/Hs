namespace HsLib.Interfaces
{
    public interface IMortal : IWithPlace, IWithDeathrattle
    {
        bool Dead { get; }
    }
}
