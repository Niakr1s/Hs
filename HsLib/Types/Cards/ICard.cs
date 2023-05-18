using HsLib.Systems;
using HsLib.Types.Places;
using HsLib.Types.Stats;

namespace HsLib.Types.Cards
{
    public interface ICard : IBattlefieldSubscriber
    {
        PlaceInContainer? PlaceInContainer { get; set; }

        Mp Mp { get; }

        bool ShouldBeRemovedFromCurrentContainer();
    }
}