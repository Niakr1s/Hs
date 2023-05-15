using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLib.Types.Containers.Base
{

    public abstract class MultiContainer<TCard> : Container<TCard>
    where TCard : ICard
    {

        protected MultiContainer(Battlefield bf, Place place,
            int? limit = null, IEnumerable<Card>? startCards = null) : base(bf, place)
        {
            Limit = limit;
            foreach (var c in startCards ?? new List<Card>(limit ?? 0)) { Add(c); }
        }

        private readonly List<ICard> _cards = new();

        public int? Limit { get; }

        protected bool IsFull
        {
            get => Limit != null && Count == Limit;
        }



        #region overrides

        public override int Count => _cards.Count;

        public override ICard this[int index] => _cards[index];

        protected override void DoInsertAt(int index, ICard card)
        {
            _cards.Insert(index, card);
        }

        protected override void DoRemove(ICard card)
        {
            _cards.Remove(card);
        }



        #endregion
    }
}
