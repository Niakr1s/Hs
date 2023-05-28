using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Places;

namespace HsLib.Types.Containers
{
    public abstract class SingleContainer<TCard> : Container<TCard>
        where TCard : ICard
    {
        protected SingleContainer(Board board, Place place, TCard startCard) :
            base(board, place, 1, new List<TCard> { startCard })
        {
        }

        public TCard Card
        {
            get
            {
                return this[0];
            }
            set
            {
                this[0] = value;
            }
        }
    }
}