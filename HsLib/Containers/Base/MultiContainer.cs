using HsLib.Battle;
using HsLib.Cards;
using HsLib.Common.Place;

namespace HsLib.Containers.Base
{

    public abstract class MultiContainer<TCard> : Container<TCard>
        where TCard : Card
    {

        protected MultiContainer(Battlefield bf, Pid pid, Loc loc,
            int? limit = null, IEnumerable<TCard>? startCards = null) : base(bf, pid, loc)
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

        public TCard? Left(TCard card)
        {
            if (_cards[0] == card) { return null; }
            int index = _cards.IndexOf(card);
            if (index == -1) { return null; }
            return _cards[index - 1];
        }

        public TCard? Right(TCard card)
        {
            if (_cards[^1] == card) { return null; }
            int index = _cards.IndexOf(card);
            if (index == -1) { return null; }
            return _cards[index + 1];
        }
    }
}
