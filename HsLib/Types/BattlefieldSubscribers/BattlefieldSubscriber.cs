using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Places;
using System.Collections.Specialized;

namespace HsLib.Types.BattlefieldSubscribers
{
    /// <summary>
    /// Is used to be source for lingering effects.
    /// </summary>
    /// <typeparam name="TOwner"></typeparam>
    /// <typeparam name="TSubscribedCard">
    /// Type of card, you want to subscribe to. Other types will be filtered out.
    /// If you don't want to sub to each individual card, just use ICard.
    /// </typeparam>
    public abstract class BattlefieldSubscriber<TOwner, TSubscribedCard> : IBattlefieldSubscriber
        where TOwner : ICard
        where TSubscribedCard : ICard
    {
        protected BattlefieldSubscriber(TOwner owner)
        {
            Owner = owner;
        }

        public bool IsActive { get; private set; }

        public TOwner Owner { get; }

        protected Battlefield? Bf { get; private set; }

        private readonly List<TSubscribedCard> _subs = new();

        /// <summary>
        /// Called on every container.
        /// </summary>
        /// <param name="bf"></param>
        public void Subscribe(Battlefield bf)
        {
            if (IsActive) { return; }
            Bf = bf;
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
            Bf = null;
            DoUnsubscribe(bf, previousPlace);
            IsActive = false;
        }

        private void DoSubscribe(Battlefield bf)
        {
            bf.CollectionChanged += Bf_CollectionChanged;
            bf.TurnEvent += Bf_TurnEvent;

            foreach (TSubscribedCard card in bf.Cards.OfType<TSubscribedCard>()) { SubscribeCard(card); }
            OnCollectionChanged();
        }

        private void DoUnsubscribe(Battlefield bf, Place previousPlace)
        {
            bf.CollectionChanged -= Bf_CollectionChanged;
            bf.TurnEvent -= Bf_TurnEvent;
            Clear();
        }

        private void Bf_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems is not null)
            {
                foreach (TSubscribedCard card in e.OldItems.OfType<TSubscribedCard>())
                {
                    UnsubscribeCard(card);
                }
            }

            if (e.NewItems is not null)
            {
                foreach (TSubscribedCard card in e.NewItems.OfType<TSubscribedCard>())
                {
                    SubscribeCard(card);
                }
            }

            OnCollectionChanged();
        }

        private void Bf_TurnEvent(object? sender, Turns.TurnEventArgs e)
        {
            switch (e.Type)
            {
                case Turns.TurnEventType.Start:
                    OnTurnStart();
                    break;
                case Turns.TurnEventType.End:
                    OnTurnEnd();
                    break;
            }
        }


        private void SubscribeCard(TSubscribedCard card)
        {
            if (ShoudBeSubscribed(card))
            {
                DoSubscribeCard(card);
                _subs.Add(card);
            }
        }

        private void UnsubscribeCard(TSubscribedCard card)
        {
            int index = _subs.IndexOf(card);
            if (index != -1)
            {
                DoUnsubscribeCard(card);
                _subs.RemoveAt(index);
            }
        }

        private void Clear()
        {
            _subs.ToList().ForEach(UnsubscribeCard);
            _subs.Clear();
        }



        /// <summary>
        /// If returns true, will be called <see cref="DoSubscribeCard(TSubscribedCard)"/>
        /// and <see cref="DoUnsubscribeCard(TSubscribedCard)"/>.
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        protected virtual bool ShoudBeSubscribed(TSubscribedCard card) { return false; }

        /// <summary>
        /// Will be called to subscribe to card, if <see cref="ShoudBeSubscribed(TSubscribedCard)"/> returned true.
        /// </summary>
        /// <param name="card"></param>
        protected virtual void DoSubscribeCard(TSubscribedCard card) { }

        /// <summary>
        /// Will be called to unsubscribe from card.
        /// </summary>
        /// <param name="card"></param>
        protected virtual void DoUnsubscribeCard(TSubscribedCard card) { }

        /// <summary>
        /// Will be fired when any other card insert or removed from collection.
        /// </summary>
        protected virtual void OnCollectionChanged() { }



        protected virtual void OnTurnEnd() { }
        protected virtual void OnTurnStart() { }
    }
}
