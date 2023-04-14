using Models.Cards;
using Models.Common;

namespace Models.Containers.Base
{
    public class SingleContainer<TCard> : Container<TCard>
        where TCard : Card
    {
        public SingleContainer(Pid pid, Loc loc, TCard card) : base(pid, loc)
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
    }
}
