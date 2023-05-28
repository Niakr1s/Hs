using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Places;

namespace HsLib.Types.BoardSubscribers
{
    public interface IBoardSubscriber
    {
        ICard Owner { get; set; }

        /// <summary>
        /// Called after implementator was inserted to container.
        /// Implementators should decide if they should subscribe by themselves.
        /// </summary>
        /// <param name="board"></param>
        void Subscribe(Board board);

        /// <summary>
        /// Called after implementator was removed from container.
        /// Implementators should decide if they should subscribe by themselves.
        /// </summary>
        /// <param name="board"></param>
        void Unsubscribe(Board board, Place previousPlace);
    }
}
