using HsLib.Types.Cards;
using HsLib.Types.Containers;
using HsLib.Types.Places;
using HsLib.Types.Turns;
using System.Collections.Specialized;

namespace HsLib.Systems
{
    public interface IBoard : INotifyCollectionChanged
    {
        ITurn Turn { get; }

        IContainer this[Place place] { get; }

        /// <summary>
        /// Should return cards in chronogical order.
        /// </summary>
        IEnumerable<ICard> Cards { get; }

        IBoardSide this[Pid pid] { get; }

        IBoardSide Enemy { get; }

        IBoardSide Player { get; }
    }
}