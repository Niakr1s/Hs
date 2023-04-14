using Models.Cards;
using Models.Common.Place;

namespace Models.Containers.Base
{

    public abstract class MultiContainer<TCard> : Container<TCard>
        where TCard : Card
    {

        protected MultiContainer(Pid pid, Loc loc,
            int? limit = null, IEnumerable<TCard>? startCards = null) : base(pid, loc)
        {
            Limit = limit;
            foreach (var c in startCards ?? new List<TCard>(limit ?? 0)) { Add(c); }
        }

        private readonly IList<TCard> _cards = new List<TCard>();

        public TCard this[int index]
        {
            get => _cards[index];
            set
            {
                RemoveAt(index);
                Insert(index, value);
            }
        }

        public override IEnumerable<TCard> Cards => _cards;

        public int? Limit { get; }

        public bool IsFull
        {
            get => Limit != null && Count == Limit;
        }

        public bool Add(TCard card)
        {
            return Insert(Count, card);
        }

        public bool Insert(int index, TCard card)
        {
            if (IsFull) { return false; }

            _cards.Insert(index, card);
            AfterInsert(card);
            return true;
        }

        public bool Remove(TCard card)
        {
            bool removed = _cards.Remove(card);
            if (!removed) { return false; }

            AfterRemove(card);
            return true;
        }

        private void RemoveAt(int index)
        {
            TCard card = _cards[index];
            _cards.RemoveAt(index);
            AfterRemove(card);
        }

        public TCard? Pop()
        {
            if (Count == 0) { return null; }

            int idx = _cards.Count - 1;
            TCard card = _cards[idx];
            _cards.RemoveAt(idx);
            AfterRemove(card);
            return card;
        }
    }
}
