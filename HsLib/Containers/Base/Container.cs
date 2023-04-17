using HsLib.Battle;
using HsLib.Cards;
using HsLib.Common.Interfaces;
using HsLib.Common.Place;
using HsLib.Events;

namespace HsLib.Containers.Base
{
    public abstract class Container<TCard> : IWithEvent<ContainerEventArgs>
        where TCard : Card
    {
        protected Container(Battlefield bf, Pid pid, Loc loc)
        {
            Bf = bf;
            Pid = pid;
            Loc = loc;
        }

        public Battlefield Bf { get; }

        public Pid Pid { get; }

        public Loc Loc { get; }

        public event EventHandler<ContainerEventArgs>? Event;

        /// <summary>
        /// Children should call this after insterting a card.
        /// </summary>
        /// <param name="card"></param>
        protected virtual void AfterInsert(TCard card)
        {
            card.Pid = Pid;
            card.Loc = Loc;
            card.TurnAdded = Bf.Turn.No;

            Event?.Invoke(this, new ContainerCardInsertEventArgs(card, Pid, Loc));
        }

        /// <summary>
        /// Children should call this after removing a card.
        /// </summary>
        /// <param name="card"></param>
        protected virtual void AfterRemove(TCard card)
        {
            card.Pid = Pid.None;
            card.Loc = Loc.None;
            card.TurnAdded = 0;

            Event?.Invoke(this, new ContainerCardRemoveEventArgs(card, Pid, Loc));
        }

        /// <summary>
        /// Should return all cards in container.
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<TCard> Cards { get; }

        public int Count => Cards.Count();

        /// <summary>
        /// Remove inactive cards from container and return them. 
        /// Each container should decide if card is inactive by itself.
        /// </summary>
        /// <returns>Cleaned cards</returns>
        public abstract IEnumerable<Card> CleanInactiveCards();
    }
}
