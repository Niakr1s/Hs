using HsLib.Systems;
using HsLib.Types.Stats;

namespace HsLib.Types.Cards
{
    public interface ICard
    {
        PlaceInContainer? Place { get; set; }

        Mp Mp { get; }

        void AfterContainerInsert(Battlefield bf);
        void AfterContainerRemove(Battlefield bf);
        void OnTurnEnd(Battlefield bf);
        void OnTurnStart(Battlefield bf);
    }
}