using HsLib.Extensions;
using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types.Events;

namespace HsLib.Types.Containers.Base
{
    public abstract class Container<TCard> : IContainer
        where TCard : ICard
    {
        protected Container(Battlefield bf, Place place)
        {
            Bf = bf;
            Place = place;
        }

        public Battlefield Bf { get; }

        public IEnumerable<TCard> CardTs => Cards.Cast<TCard>();



        #region IContainer implementation

        public event EventHandler<ContainerEventArgs>? Event;



        public Place Place { get; }

        public IEnumerable<ICard> Cards
        {
            get
            {
                for (int i = 0; i < Count; i++)
                {
                    yield return this[i];
                }
            }
        }

        public abstract ICard this[int index] { get; }

        public abstract int Count { get; }



        public bool Add(ICard card)
        {
            try
            {
                Insert(Count, card);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Insert(int index, ICard card)
        {
            DoInsertAt(index, card);
            card.Place = Place.InContainer(index, Bf.Turn.No);

            UpdateCardsPlaces();
            Event?.Invoke(this, new ContainerCardInsertEventArgs(card));
        }

        public bool CanBeInsertedAt(int index) => index >= 0 && index <= Count;

        public bool Contains(ICard card) => Cards.Contains(card);



        public RemovedCard Replace(int index, ICard card)
        {
            RemovedCard removedCard = RemoveAt(index);
            Insert(index, card);
            return removedCard;
        }

        public RemovedCard Remove(ICard card)
        {
            int index = Cards.ToList().IndexOf(card);
            return RemoveAt(index);
        }

        public RemovedCard RemoveAt(int index)
        {
            ICard card = DoRemoveAt(index);
            card.Place = null;

            UpdateCardsPlaces();
            Event?.Invoke(this, new ContainerCardRemoveEventArgs(card, Place));
            return new RemovedCard(card, Place);
        }

        public RemovedCard? Pop()
        {
            try
            {
                return RemoveAt(Count - 1);
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<RemovedCard> RemoveIf(Predicate<ICard> predicate)
        {
            List<ICard> cardsToRemove = new();
            foreach (ICard card in Cards)
            {
                if (predicate(card))
                {
                    cardsToRemove.Add(card);
                }
            }

            List<RemovedCard> cleanedCards = new();
            foreach (ICard card in cardsToRemove)
            {
                Remove(card);
                RemovedCard cleanedCard = new(card, Place);
                cleanedCards.Add(cleanedCard);
            }

            return cleanedCards.AsEnumerable();
        }

        public IEnumerable<RemovedCard> RemoveInactiveCards() => RemoveIf(c => !IsCardActive(c));



        public ICard? Left(int index)
        {
            try
            {
                return this[index - 1];
            }
            catch
            {
                return null;
            }
        }

        public ICard? Right(int index)
        {
            try
            {
                return this[index + 1];
            }
            catch
            {
                return null;
            }
        }



        public Action MoveToContainer(int fromIndex, IContainer toContainer, bool canBurn, int? toIndex = null, ICard? transformTo = null)
        {
            ICard? fromCard = this[fromIndex]; // this can throw IndexOutOfRangeException
            toIndex ??= toContainer.Count;

            bool canBeInserted = toContainer.CanBeInsertedAt(toIndex.Value);
            if (!canBeInserted && !canBurn)
            {
                throw new InvalidOperationException($"can't be inserted container {toContainer}");
            }
            // check section ended

            ICard toCard = transformTo ?? fromCard;

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



        #region private helpers

        /// <summary>
        /// Should update cards places.
        /// </summary>
        private void UpdateCardsPlaces()
        {
            foreach (var (card, index) in Cards.WithIndex())
            {
                card.Place = card.Place! with { Index = index };
            }
        }

        #endregion



        #region protected

        /// <summary>
        /// Inserts card at index.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="card"></param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        /// <exception cref="ContainerInsertException"></exception>
        /// <returns>inserted card</returns>
        protected abstract void DoInsertAt(int index, ICard card);

        /// <summary>
        /// Removes card from container.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        /// <returns>removed card</returns>
        protected abstract ICard DoRemoveAt(int index);

        /// <summary>
        /// Should check if card in container at index is invalid and should be removed.
        /// </summary>
        protected virtual bool IsCardActive(ICard card) { return true; }

        #endregion
    }

    public record RemovedCard(ICard Card, Place Place);

    public class ContainerInsertException : Exception
    {
        public ContainerInsertException() : base()
        {
        }

        public ContainerInsertException(string? message) : base(message)
        {
        }

        public ContainerInsertException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
