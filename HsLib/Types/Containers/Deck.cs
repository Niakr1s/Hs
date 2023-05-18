using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Places;

namespace HsLib.Types.Containers
{
    public class Deck : Container<IPlayableFromHand>
    {
        public Deck(Battlefield bf, Pid pid, IEnumerable<IPlayableFromHand>? startCards = null) :
            base(bf, new Place(pid, Loc.Deck), startCards: startCards)
        {
        }

        /// <summary>
        /// Takes next card and puts it at hand if possible. Burn otherwise.
        /// </summary>
        public void TakeNextCard()
        {
            var removedCard = Pop();
            if (removedCard is null) { return; }

            Hand hand = Bf[Place.Pid].Hand;
            if (!hand.IsFull)
            {
                Bf[Place.Pid].Hand.Add(removedCard);
            }
        }
    }
}
