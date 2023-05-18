using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Places;

namespace HsLib.Types.Containers
{
    public abstract class SingleContainer<TCard> : Container<TCard>
        where TCard : ICard
    {
        protected SingleContainer(Battlefield bf, Place place, TCard card) : base(bf, place, limit: 1)
        {
            Add(card);
        }

        public TCard Card
        {
            get => this[0];
            set => Set(value);
        }

        public RemovedCard Set(TCard card)
        {
            TCard oldCard = this[0];
            this[0] = card;
            return new RemovedCard(oldCard, Place);
        }

        public override bool CanBeInserted => true;

        /// <summary>
        /// Inserts an element at specific index.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public override void Insert(int index, TCard item)
        {
            if (index != 0) { base.Insert(index, item); }
            this[index] = item;
        }
    }
}
