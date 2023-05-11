using HsLib.Common.Place;

namespace HsLib.Common.MeleeAttack
{
    public interface IMortal : IWithPlace, IWithDeathrattle
    {
        bool Dead { get; }
    }
}
