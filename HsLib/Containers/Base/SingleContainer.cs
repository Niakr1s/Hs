using HsLib.Battle;
using HsLib.Cards;
using HsLib.Common.Place;

namespace HsLib.Containers.Base
{
    public abstract class SingleContainer<TCard> : Container<TCard>
        where TCard : Card
    {
        public SingleContainer(Battlefield bf, Pid pid, Loc loc, TCard card) : base(bf, pid, loc)
        {
            _card = card;
            AfterInsert(card);
        }

        TCard _card;
        public TCard Card
        {
            get => _card;
            set
            {
                if (_card == value)
                {
                    return;
                }

                var prev = _card;
                _card = value;

                AfterRemove(prev);
                AfterInsert(value);
            }
        }

        public override IEnumerable<TCard> Cards
        {
            get { yield return Card; }
        }

        public override TCard this[int index]
        {
            get
            {
                if (index != 0) { throw new IndexOutOfRangeException(); }
                return Card;
            }
        }

        public override bool CanBeInsertedAt(int index)
        {
            return index == 0;
        }
    }
}
