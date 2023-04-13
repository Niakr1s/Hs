using Models.Cards;
using Models.Common;

namespace Models.Containers.Container
{
    public abstract class Container<TCard>
        where TCard : Card
    {
        protected Container(Battlefield bf, Pid pid, Loc loc)
        {
            Bf = bf;
            Pid = pid;
            Loc = loc;
        }

        public Loc Loc { get; }

        public Battlefield Bf { get; }

        public Pid Pid { get; }

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
