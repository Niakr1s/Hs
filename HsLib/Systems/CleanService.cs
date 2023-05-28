using HsLib.Types.Containers;

namespace HsLib.Systems
{
    public class CleanService
    {
        public CleanService(Battlefield bf)
        {
            Bf = bf;
        }

        public Battlefield Bf { get; }

        /// <summary>
        /// Removes dead cards from containers in insert order.
        /// </summary>
        /// <returns>Removed cards.</returns>
        public IEnumerable<RemovedCard> CleanInactiveCards()
        {
            IEnumerable<RemovedCard> cardsToClean = Bf.Cards.Where(c => c.ShouldBeCleaned())
                .Select(c => new RemovedCard(c, c.Place)).ToList();

            foreach (RemovedCard card in cardsToClean)
            {
                Bf[card.Place.Pid].Remove(card.Card);
            }
            return cardsToClean.AsEnumerable();
        }
    }
}