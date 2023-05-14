using HsLib.Extensions;
using HsLib.Systems;
using HsLib.Types.Cards;
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



        #region public

        public Battlefield Bf { get; }

        public Place Place { get; }

        public event EventHandler<ContainerEventArgs>? Event;

        /// <summary>
        /// Should return card at index
        /// </summary>
        /// <param name="index"></param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        /// <returns>card in container</returns>
        public abstract ICard this[int index] { get; }

        /// <summary>
        /// Should return actual container length.
        /// </summary>
        public abstract int Count { get; }

        /// <summary>
        /// Main method, all other should use this.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="card"></param>
        public void Insert(int index, ICard card)
        {
            DoInsertAt(index, card);
            card.Place = Place.InContainer(index, Bf.Turn.No);

            UpdateCardsPlaces();
            Event?.Invoke(this, new ContainerCardInsertEventArgs(card));
        }

        /// <summary>
        /// Main method, all other should use this
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public RemovedCard Remove(ICard card)
        {
            DoRemove(card);
            card.Place = null;

            UpdateCardsPlaces();
            Event?.Invoke(this, new ContainerCardRemoveEventArgs(card, Place));
            return new RemovedCard(card, Place);
        }

        public RemovedCard Replace(int index, ICard card)
        {
            RemovedCard removedCard = RemoveAt(index);
            Insert(index, card);
            return removedCard;
        }

        public RemovedCard RemoveAt(int index)
        {
            return Remove(this[index]);
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

        public bool CanBeInsertedAt(int index)
        {
            return index >= 0 && index <= Count;
        }

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

        public IEnumerable<TCard> CardTs => Cards.Select(c => (TCard)c);

        /// <summary>
        /// Remove inactive cards from container and return them. 
        /// Each container should decide if card is inactive by itself.
        /// </summary>
        /// <returns>Cleaned cards</returns>
        public IEnumerable<RemovedCard> RemoveInactiveCards()
        {
            return RemoveIf(c => !IsCardActive(c));
        }

        #endregion



        #region private

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
        /// <param name="card"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns>removed card</returns>
        protected abstract void DoRemove(ICard card);

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
