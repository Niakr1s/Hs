using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Places;

namespace HsLib.Types.LingeringEffects
{
    public abstract class LingeringEffectSource<TCard> : IBattlefieldSubscriber
        where TCard : ICard
    {
        protected LingeringEffectSource(TCard owner)
        {
            Owner = owner;
        }

        public bool IsActive { get; private set; }

        public TCard Owner { get; }

        /// <summary>
        /// Called on every container.
        /// </summary>
        /// <param name="bf"></param>
        public void Subscribe(Battlefield bf)
        {
            if (IsActive) { return; }
            DoSubscribe(bf);
            IsActive = true;
        }

        /// <summary>
        /// Deactivates aura.
        /// </summary>
        /// <param name="bf"></param>
        /// <exception cref="Exception">Throws, if anything unexpected occurs.</exception>
        /// <returns>True, if was success deactivated.</returns>
        public void Unsubscribe(Battlefield bf, Place previousPlace)
        {
            if (!IsActive) { return; }
            DoUnsubscribe(bf, previousPlace);
            IsActive = false;
        }


        protected abstract void DoSubscribe(Battlefield bf);
        protected abstract void DoUnsubscribe(Battlefield bf, Place previousPlace);
    }
}
