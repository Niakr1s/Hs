using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLib.Types.Containers.Base
{
    public abstract class SingleContainer<TCard> : MultiContainer<TCard>
        where TCard : ICard
    {
        protected SingleContainer(Battlefield bf, Place place, TCard card) : base(bf, place, limit: 1)
        {
            Add(card);
        }

        public TCard Card
        {
            get
            {
                return (TCard)this[0];
            }
            set
            {
                Set(value);
            }
        }

        public RemovedCard Set(TCard card)
        {
            return Replace(0, card);
        }
    }
}
