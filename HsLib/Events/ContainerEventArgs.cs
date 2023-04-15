using HsLib.Cards;
using HsLib.Common.Place;

namespace HsLib.Events
{
    public abstract class ContainerEventArgs : EventArgs
    {
        protected ContainerEventArgs(Card card, Pid pid, Loc loc)
        {
            Card = card;
            Pid = pid;
            Loc = loc;
        }

        public Card Card { get; }

        public Pid Pid { get; }

        public Loc Loc { get; }
    }

    public class ContainerCardAddedEventArgs : ContainerEventArgs
    {
        public ContainerCardAddedEventArgs(Card card, Pid pid, Loc loc) : base(card, pid, loc)
        {

        }
    }

    public class ContainerCardRemovedEventArgs : ContainerEventArgs
    {
        public ContainerCardRemovedEventArgs(Card card, Pid pid, Loc loc) : base(card, pid, loc)
        {

        }
    }
}
