using HsLib.Systems;
using HsLib.Types.Places;

namespace HsLib.Types.BattlefieldSubscribers
{
    public interface IBattlefieldSubscriber
    {
        /// <summary>
        /// Called after implementator was inserted to container.
        /// Implementators should decide if they should subscribe by themselves.
        /// </summary>
        /// <param name="bf"></param>
        void Subscribe(Battlefield bf);

        /// <summary>
        /// Called after implementator was removed from container.
        /// Implementators should decide if they should subscribe by themselves.
        /// </summary>
        /// <param name="bf"></param>
        void Unsubscribe(Battlefield bf, Place previousPlace);
    }
}
