using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Places;
using System.Collections.Specialized;

namespace HsLib.Types.BoardSubscribers
{
    /// <summary>
    /// Is used to be source for lingering effects.
    /// </summary>
    /// <typeparam name="TSubscribedCard">
    /// Type of card, you want to subscribe to. Other types will be filtered out.
    /// If you don't want to sub to each individual card, just use ICard.
    /// </typeparam>
    public abstract class BoardSubscriber<TSubscribedCard> : IBoardSubscriber
        where TSubscribedCard : ICard
    {
        protected BoardSubscriber(ICard owner)
        {
            Owner = owner;
        }

        public ICard Owner { get; set; }

        protected Board? Board { get; private set; }

        private readonly List<TSubscribedCard> _subs = new();

        /// <summary>
        /// Called on every container.
        /// </summary>
        /// <param name="board"></param>
        public void Subscribe(Board board)
        {
            Board = board;
            DoSubscribe(board);
        }

        /// <summary>
        /// Deactivates aura.
        /// </summary>
        /// <param name="board"></param>
        /// <exception cref="Exception">Throws, if anything unexpected occurs.</exception>
        /// <returns>True, if was success deactivated.</returns>
        public void Unsubscribe(Board board, Place previousPlace)
        {
            Board = null;
            DoUnsubscribe(board, previousPlace);
        }

        private void DoSubscribe(Board board)
        {
            board.CollectionChanged += Board_CollectionChanged;
            board.TurnEvent += Board_TurnEvent;

            foreach (TSubscribedCard card in board.Cards.OfType<TSubscribedCard>()) { SubscribeCard(card); }
            OnCollectionChanged();
        }

        private void DoUnsubscribe(Board board, Place previousPlace)
        {
            board.CollectionChanged -= Board_CollectionChanged;
            board.TurnEvent -= Board_TurnEvent;
            Clear();
        }

        private void Board_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
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

        private void Board_TurnEvent(object? sender, Turns.TurnEventArgs e)
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
