using HsLib.Systems;
using HsLib.Types;
using HsLib.Types.Stats;

namespace HsLib.Interfaces
{
    public interface ICard : IBattlefieldSubscriber
    {
        PlaceInContainer? Place { get; set; }

        Mp Mp { get; }

        void AfterContainerInsert(Battlefield bf);
        void AfterContainerRemove(Battlefield bf);
    }
}