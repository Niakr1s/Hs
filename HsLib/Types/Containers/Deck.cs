using HsLib.Interfaces.CardTraits;
using HsLib.Systems;
using HsLib.Types.Containers.Base;

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

            Bf[Place.Pid].Hand.Add(removedCard);
        }
    }
}
