using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Places;

namespace HsLib.Types.Containers
{
    public abstract class SingleMaybeContainer<TCard> : Container<TCard>
        where TCard : ICard
    {
        protected SingleMaybeContainer(Board board, Place place) : base(board, place)
        {
        }

        public TCard? Card
        {
            get
            {
                try
                {
                    return this[0];
                }
                catch
                {
                    return default;
                }
            }
            set
            {
                if (value is null)
                {
                    Clear();
                }
                else
                {
                    if (Count == 0)
                    {
                        Add(value);
                    }
                    else
                    {
                        this[0] = value;
                    }
                }
            }
        }
    }
}