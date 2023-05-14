using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Events;

namespace HsLib.Types.Containers.Base
{
    public interface IContainer : IWithEvent<ContainerEventArgs>
    {
        Battlefield Bf { get; }
        Place Place { get; }

        ICard this[int index] { get; }
        IEnumerable<ICard> Cards { get; }
        int Count { get; }

        bool Add(ICard card);
        void Insert(int index, ICard card);
        RemovedCard Replace(int index, ICard card);
        RemovedCard RemoveAt(int index);
        RemovedCard? Pop();
        bool CanBeInsertedAt(int index);

        ICard? Left(int index);
        ICard? Right(int index);

        IEnumerable<RemovedCard> RemoveIf(Predicate<ICard> predicate);
        IEnumerable<RemovedCard> RemoveInactiveCards();
        Action MoveToContainer(int fromIndex, IContainer toContainer, bool canBurn, int? toIndex = null, ICard? transformTo = null);
    }
}