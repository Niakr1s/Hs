using Models.Cards;
using Models.Common;

namespace Models.Events
{
    public abstract class ContainerEventArgs : EventArgs
    {
        protected ContainerEventArgs(Card card, Pid pid, Loc loc)
        {
            this.Card = card;
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
