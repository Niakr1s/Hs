using HsLib.Types.Cards;
using HsLib.Types.Places;
using System.Collections;
using System.Collections.Specialized;

namespace HsLib.Types.Containers
{
    // todo: make readonly
    public interface IContainer : IList, IWithPlace, INotifyCollectionChanged
    {
        bool CanBeInserted { get; }

        void CleanInactiveCards(); // todo to readonly interface

        // todo to readonly interface
        Action MoveToContainer(ICard card, IContainer toContainer, bool canBurn, int? toIndex = null);

        ICard? Left(ICard card);

        ICard? Right(ICard card);
    }
}