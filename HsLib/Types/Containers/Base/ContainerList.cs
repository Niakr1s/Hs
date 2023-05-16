using HsLib.Interfaces;
using System.Collections.Specialized;
using System.Data;

namespace HsLib.Types.Containers.Base
{
    public class ContainerList : INotifyCollectionChanged
    {
        public ContainerList(IEnumerable<IContainer> containers)
        {
            _containers = containers.ToList();

            ForEach(c => c.CollectionChanged += ConnectEvents);
        }

        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        private void ConnectEvents(object? sender, NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(sender, e);
        }

        private readonly List<IContainer> _containers;

        public IEnumerable<IContainer> Containers => _containers.AsEnumerable();

        public IEnumerable<ICard> Cards
        {
            get
            {
                foreach (var container in _containers)
                {
                    foreach (var card in container)
                    {
                        yield return (ICard)card;
                    }
                }
            }
        }

        public void ForEach(Action<IContainer> action)
        {
            foreach (IContainer container in Containers) { action(container); }
        }

        public ICard GetCard(Loc loc, int index)
        {
            foreach (IContainer container in _containers)
            {
                if (container.Place.Loc == loc)
                {
                    var card = container[index];
                    return card is null ? throw new ArgumentException("wrong index") : (ICard)card;
                }
            }

            throw new ArgumentException("wrong loc");
        }

        public void CleanInactiveCards()
        {
            foreach (IContainer container in Containers)
            {
                container.CleanInactiveCards();
            }
        }
    }
}
