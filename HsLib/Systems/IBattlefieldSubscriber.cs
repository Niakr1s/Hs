using HsLib.Types.Places;

namespace HsLib.Systems
{
    public interface IBattlefieldSubscriber
    {
        void Subscribe(Battlefield bf);
        void Unsubscribe(Battlefield bf, Place previousPlace);
    }
}
