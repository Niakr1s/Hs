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

    public class ContainerCardInsertEventArgs : ContainerEventArgs
    {
        public ContainerCardInsertEventArgs(Card card, Pid pid, Loc loc) : base(card, pid, loc)
        {

        }
    }

    public class ContainerCardRemoveEventArgs : ContainerEventArgs
    {
        public ContainerCardRemoveEventArgs(Card card, Pid pid, Loc loc) : base(card, pid, loc)
        {

        }
    }
}
