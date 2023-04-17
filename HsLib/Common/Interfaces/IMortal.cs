using HsLib.Common.Place;

namespace HsLib.Common.Interfaces
{
    public interface IMortal : IWithPlace, IWithDeathrattle
    {
        bool Dead { get; }
    }
}
