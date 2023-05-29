using HsLib.Types.Cards;
using HsLib.Types.Containers;
using HsLib.Types.Places;
using HsLib.Types.Turns;
using System.Collections.Specialized;

namespace HsLib.Systems
{
    public interface IBoard : INotifyCollectionChanged
    {
        IBoardSide this[Pid pid] { get; }
        IContainer this[Place place] { get; }

        IEnumerable<ICard> Cards { get; }
        IBoardSide Enemy { get; }
        IBoardSide Player { get; }

        event EventHandler<TurnEventArgs>? TurnEvent;
    }
}