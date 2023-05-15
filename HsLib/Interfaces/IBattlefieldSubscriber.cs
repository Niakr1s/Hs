using HsLib.Systems;

namespace HsLib.Interfaces
{
    public interface IBattlefieldSubscriber
    {
        void AfterContainerInsert(Battlefield bf);
        void AfterContainerRemove(Battlefield bf);
        void OnTurnEnd(Battlefield bf);
        void OnTurnStart(Battlefield bf);
    }
}
