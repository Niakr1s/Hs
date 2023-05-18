using HsLib.Types;
using HsLib.Types.Stats;

namespace HsLib.Interfaces
{
    public interface ICard : IBattlefieldSubscriber
    {
        PlaceInContainer? PlaceInContainer { get; set; }

        Mp Mp { get; }

        bool ShouldBeRemovedFromCurrentContainer();
    }
}