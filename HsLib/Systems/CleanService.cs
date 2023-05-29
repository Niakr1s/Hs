using HsLib.Types.Containers;

namespace HsLib.Systems
{
    public class CleanService : Service
    {
        public CleanService(Board board) : base(board)
        {
        }

        /// <summary>
        /// Removes dead cards from containers in insert order.
        /// </summary>
        /// <returns>Removed cards.</returns>
        public IEnumerable<RemovedCard> CleanInactiveCards()
        {
            IEnumerable<RemovedCard> cardsToClean = Board.Cards.Where(c => c.ShouldBeCleaned())
                .Select(c => new RemovedCard(c, c.Place)).ToList();

            foreach (RemovedCard card in cardsToClean)
            {
                Board[card.Place.Pid].Remove(card.Card);
            }
            return cardsToClean.AsEnumerable();
        }
    }
}