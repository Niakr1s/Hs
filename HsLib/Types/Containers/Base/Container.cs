using HsLib.Extensions;
using HsLib.Interfaces;
using HsLib.Systems;
using System.Collections.ObjectModel;

namespace HsLib.Types.Containers.Base
{
    public class Container<TCard> : ObservableCollection<TCard>, IContainer
        where TCard : ICard
    {
        #region ctors

        public Container(Battlefield bf, Place place, int? limit = null, IEnumerable<TCard>? startCards = null)
        {
            Bf = bf;
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
            UpdateCardsPlaces();

            if (e.NewItems is not null)
            {
                foreach (TCard item in e.NewItems)
                {
                    item.Place = Place.InContainer(Bf.Turn.No, IndexOf(item));
                    item.AfterContainerInsert(Bf);
                }
            }

            if (e.OldItems is not null)
            {
                foreach (TCard item in e.OldItems)
                {
                    item.Place = default;
                    item.AfterContainerRemove(Bf);
                }
            }
        }

        public Battlefield Bf { get; }

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

        public Action MoveToContainer(int fromIndex, IContainer toContainer, bool canBurn, int? toIndex = 0, object? transformTo = default)
        {
            TCard? fromCard = this[fromIndex]; // this can throw IndexOutOfRangeException
            toIndex ??= toContainer.Count;

            bool canBeInserted = toContainer.CanBeInserted;
            if (!canBeInserted && !canBurn)
            {
                throw new InvalidOperationException($"can't be inserted container {toContainer}");
            }
            // check section ended

            object? toCard = transformTo ?? fromCard;

            return () =>
            {
                RemoveAt(fromIndex);
                if (canBeInserted)
                {
                    toContainer.Insert(toIndex.Value, toCard);
                }
            };
        }

        #endregion


        #region helpers 

        private void CollectionIsFullCheck()
        {
            if (IsFull) { throw new InvalidOperationException("collection is full"); }
        }

        private void UpdateCardsPlaces()
        {
            foreach (var (card, index) in this.WithIndex())
            {
                if (card.Place is not null)
                {
                    card.Place = card.Place with { Index = index };
                }
            }
        }

        #endregion
    }
}
