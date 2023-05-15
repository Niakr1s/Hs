using HsLib.Interfaces;
using HsLib.Types.Events;

namespace HsLib.Types.Containers.Base
{
    public interface IContainer : IWithEvent<ContainerEventArgs>
    {
        #region getters

        Place Place { get; }

        IEnumerable<ICard> Cards { get; }

        /// <summary>
        /// Should return card at index
        /// </summary>
        /// <param name="index"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>card in container</returns>
        ICard this[int index] { get; }

        /// <summary>
        /// Should return actual container length.
        /// </summary>
        int Count { get; }

        #endregion


        #region insert

        /// <summary>
        /// Adds card to container.
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        bool Add(ICard card);

        /// <summary>
        /// Main method, all other should use this.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="card"></param>
        void Insert(int index, ICard card);

        /// <summary>
        /// Shows, if card can be inserted at container.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        bool CanBeInsertedAt(int index);

        /// <summary>
        /// Shows if card contains at container.
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        bool Contains(ICard card);

        #endregion



        #region remove

        /// <summary>
        /// Replaces card with new card.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="card"></param>
        /// <returns></returns>
        RemovedCard Replace(int index, ICard card);

        /// <summary>
        /// Removes card.
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        RemovedCard Remove(ICard card);

        /// <summary>
        /// Removes card at index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        RemovedCard RemoveAt(int index);

        /// <summary>
        /// Pops right/top-most card.
        /// </summary>
        /// <returns></returns>
        RemovedCard? Pop();

        /// <summary>
        /// Removes card if predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<RemovedCard> RemoveIf(Predicate<ICard> predicate);

        /// <summary>
        /// Remove inactive cards from container and return them. 
        /// Each container should decide if card is inactive by itself.
        /// </summary>
        /// <returns>Cleaned cards</returns>
        IEnumerable<RemovedCard> RemoveInactiveCards();

        #endregion



        /// <summary>
        /// Gets left card.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        ICard? Left(int index);

        /// <summary>
        /// Gets right card.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        ICard? Right(int index);



        /// <summary>
        /// Removes card from container and inserts at another. First call just throws exceptions if move is impossible.
        /// Calling returned action actually does movement.
        /// </summary>
        /// <param name="fromIndex"></param>
        /// <param name="toContainer"></param>
        /// <param name="canBurn">instead of throwing exception on insert failure, just discards</param>
        /// <param name="toIndex">defaults to last</param>
        /// <param name="transformTo"></param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="IndexOutOfRangeException"></exception>
        /// <returns>Action, that actually moves to container.</returns>
        Action MoveToContainer(int fromIndex, IContainer toContainer, bool canBurn, int? toIndex = null, ICard? transformTo = null);
    }
}