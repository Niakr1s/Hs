using HsLib.Systems;
using HsLib.Types.Places;
using System.Collections;
using System.Collections.Specialized;

namespace HsLib.Types.Containers
{
    public interface IContainer : IList, IWithPlace, INotifyCollectionChanged
    {
        Battlefield Bf { get; }

        bool CanBeInserted { get; }

        void CleanInactiveCards();

        Action MoveToContainer(int fromIndex, IContainer toContainer, bool canBurn, int? toIndex = null, object? transformTo = default);
    }
}