using HsLib.Types.Cards;
using System.Data;

namespace HsLib.Types.Containers.Base
{
    public class ContainerList
    {

        public ContainerList(IEnumerable<IContainer> containers)
        {
            _containers = containers.ToList();
        }

        private readonly List<IContainer> _containers;

        public IEnumerable<IContainer> Containers => _containers.AsEnumerable();

        public IEnumerable<ICard> Cards => Containers.SelectMany(x => x.Cards);

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
                    return container[index];
                }
            }

            throw new ArgumentException("wrong loc or index");
        }

        public IEnumerable<RemovedCard> RemoveInactiveCards()
        {
            List<RemovedCard> removedCards = new();
            foreach (IContainer container in Containers)
            {
                removedCards.AddRange(container.RemoveInactiveCards().ToList());
            }
            return removedCards.AsEnumerable();
        }
    }
}
