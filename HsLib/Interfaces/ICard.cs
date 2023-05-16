using HsLib.Types;
using HsLib.Types.Stats;

namespace HsLib.Interfaces
{
    public interface ICard : IBattlefieldSubscriber
    {
        PlaceInContainer? Place { get; set; }

        Mp Mp { get; }
    }
}