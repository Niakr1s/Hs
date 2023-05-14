using HsLib.Types.Cards;

namespace HsLib.Types.Events
{
    public abstract class ContainerEventArgs : EventArgs
    {
    }

    public class ContainerCardInsertEventArgs : ContainerEventArgs
    {
        public ContainerCardInsertEventArgs(ICard card)
        {
            Card = card;
        }

        public ICard Card { get; }
    }

    public class ContainerCardRemoveEventArgs : ContainerEventArgs
    {
        public ContainerCardRemoveEventArgs(ICard card, Place place)
        {
            Card = card;
            Place = place;
        }

        public ICard Card { get; }
        public Place Place { get; }
    }
}
