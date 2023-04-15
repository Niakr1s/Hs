using HsLib.Cards;
using HsLib.Common.Place;
using HsLib.Events;

namespace HsLib.Containers.Base
{
    public abstract class Container<TCard>
        where TCard : Card
    {
        protected Container(Battlefield bf, Pid pid, Loc loc)
        {
            Bf = bf;
            Pid = pid;
            Loc = loc;

            Bf.Turn.Event += Turn_Event;
        }

        public Battlefield Bf { get; }

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
            card.TurnAdded = Bf.Turn.No;
            card.AfterContainerInsert(Bf);
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
            card.AfterContainerRemove(Bf);
        }

        /// <summary>
        /// Should return all cards in container.
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<TCard> Cards { get; }

        public int Count => Cards.Count();

        private void Turn_Event(object? sender, TurnEventArgs e)
        {
            switch (e)
            {
                case TurnStartEventArgs:
                    foreach (var c in Cards) c.OnTurnStart(Bf);
                    break;
                case TurnEndEventArgs:
                    foreach (var c in Cards) c.OnTurnEnd(Bf);
                    break;
            }
        }
    }
}
