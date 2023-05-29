using HsLib.Types.Containers;

namespace HsLib.Systems
{
    public class CleanService : Service
    {
        public CleanService(Board board) : base(board)
        {
        }

        /// <summary>
        /// Removes inactive cards from containers in insert order.
        /// </summary>
        /// <returns>Removed cards.</returns>
        public void CleanInactiveCards()
        {
            while (DoCleanStep()) { }
        }

        /// <summary>
        /// Do cleaning step.
        /// </summary>
        /// <returns>True, if step should repeat</returns>
        private bool DoCleanStep()
        {
            List<RemovedCard> cardsToClean = Board.Cards.Where(c => c.ShouldBeCleaned())
                .Select(c => new RemovedCard(c, c.Place)).ToList();

            foreach (RemovedCard card in cardsToClean)
            {
                Board[card.Place.Pid].Remove(card.Card);
            }

            return cardsToClean.Count != 0;
        }
    }
}