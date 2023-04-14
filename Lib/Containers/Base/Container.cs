using Models.Cards;
using Models.Common;
using Models.Containers.Events;

namespace Models.Containers.Base
{
    public abstract class Container<TCard>
        where TCard : Card
    {
        protected Container(Pid pid, Loc loc)
        {
            Pid = pid;
            Loc = loc;
        }

        public Pid Pid { get; }

        public Loc Loc { get; }

        public EventHandler<ContainerEventArgs>? Event { get; }

        /// <summary>
        /// Children should call this after insterting a card.
        /// </summary>
        /// <param name="card"></param>
        protected virtual void AfterInsert(TCard card)
        {
            card.Pid = Pid;
            card.Loc = Loc;
        }

        /// <summary>
        /// Children should call this after removing a card.
        /// </summary>
        /// <param name="card"></param>
        protected virtual void AfterRemove(TCard card)
        {
            card.Pid = Pid.None;
            card.Loc = Loc.None;
        }

        /// <summary>
        /// Should return all cards in container.
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<TCard> Cards { get; }

        public int Count => Cards.Count();
    }
}
