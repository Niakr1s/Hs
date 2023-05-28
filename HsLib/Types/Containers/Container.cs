using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Places;
using System.Collections.ObjectModel;

namespace HsLib.Types.Containers
{
    public class Container<TCard> : ObservableCollection<TCard>, IContainer
        where TCard : ICard
    {
        #region ctors

        public Container(Board board, Place place, int? limit = null, IEnumerable<TCard>? startCards = null)
        {
            Board = board;
            Place = place;
            Limit = limit;
            CollectionChanged += OnCollectionChanged;

            if (startCards is not null)
            {
                foreach (var card in startCards) { Add(card); }
            }
        }

        #endregion

        private void OnCollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems is not null)
            {
                foreach (TCard item in e.NewItems)
                {
                    item.Place = Place;
                    item.Subscribe(Board);
                }
            }

            if (e.OldItems is not null)
            {
                foreach (TCard item in e.OldItems)
                {
                    item.Place = new();
                    item.Unsubscribe(Board, Place);
                }
            }
        }

        public Board Board { get; }

        public Place Place { get; }

        public int? Limit { get; }

        public bool IsFull => Limit != null && Count == Limit;

        public virtual bool CanBeInserted => !IsFull;



        #region ObservableCollection overrides

        /// <summary>
        /// Adds an object to the end of collection.
        /// </summary>
        /// <param name="item"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public new void Add(TCard item)
        {
            CollectionIsFullCheck();
            base.Add(item);
        }

        /// <summary>
        /// Inserts an element at specific index.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public new virtual void Insert(int index, TCard item)
        {
            CollectionIsFullCheck();
            base.Insert(index, item);
        }

        #endregion



        #region public

        public TCard? Pop()
        {
            int lastIndex = Count - 1;
            if (lastIndex < 0) { return default; }

            TCard removedCard = this[lastIndex];
            RemoveAt(lastIndex);
            return removedCard;
        }

        public void CleanInactiveCards() => RemoveIf(c => !IsCardActive(c));

        protected virtual bool IsCardActive(TCard card) => true;

        public void RemoveIf(Predicate<TCard> predicate)
        {
            foreach (TCard card in this.Where(c => predicate(c)).ToList())
            {
                { Remove(card); }
            }
        }

        public bool Remove(ICard card)
        {
            if (card is TCard tcard)
            {
                return base.Remove(tcard);
            }
            else
            {
                return false;
            }
        }

        public TCard? Left(int index)
        {
            try
            {
                return this[index - 1];
            }
            catch
            {
                return default;
            }
        }

        public ICard? Left(ICard card)
        {
            if (card is TCard tcard)
            {
                int index = IndexOf(tcard);
                return Left(index);
            }
            else
            {
                return null;
            }
        }

        public TCard? Right(int index)
        {
            try
            {
                return this[index + 1];
            }
            catch
            {
                return default;
            }
        }

        public ICard? Right(ICard card)
        {
            if (card is TCard tcard)
            {
                int index = IndexOf(tcard);
                return Right(index);
            }
            else
            {
                return null;
            }
        }

        public Action MoveToContainer(ICard card, IContainer toContainer, bool canBurn, int? toIndex = 0)
        {
            return MoveToContainer((TCard)card, toContainer, canBurn, toIndex);
        }

        public Action MoveToContainer(TCard card, IContainer toContainer, bool canBurn, int? toIndex = 0)
        {

            int fromIndex = IndexOf(card);
            if (fromIndex == -1) { throw new ValidationException("card doesn't belong to this container"); }

            toIndex ??= toContainer.Count;

            bool canBeInserted = toContainer.CanBeInserted;
            if (!canBeInserted && !canBurn)
            {
                throw new InvalidOperationException($"can't be inserted container {toContainer}");
            }
            // check section ended

            return () =>
            {
                var card = this[fromIndex];
                RemoveAt(fromIndex);
                if (canBeInserted)
                {
                    toContainer.Insert(toIndex.Value, card);
                }
            };
        }

        #endregion


        #region helpers 

        private void CollectionIsFullCheck()
        {
            if (IsFull) { throw new InvalidOperationException("collection is full"); }
        }

        #endregion
    }
}
