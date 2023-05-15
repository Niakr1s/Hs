using HsLib.Interfaces.CardTraits;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Containers.Base;

namespace HsLib.Types.Containers
{
    public class Deck : MultiContainer<IPlayableFromHand>
    {
        public Deck(Battlefield bf, Pid pid, IEnumerable<Card>? cards = null) : base(bf, new Place(pid, Loc.Deck), startCards: cards)
        {
        }

        /// <summary>
        /// Takes next card and puts it at hand if possible. Burn otherwise.
        /// </summary>
        /// <returns></returns>
        public void TakeNextCard()
        {
            RemovedCard? removedCard = Pop();
            if (removedCard is null) { return; }

            Bf[Place.Pid].Hand.Add(removedCard.Card);
        }
    }
}
