using HsLib.Types.Containers;

namespace HsLib.Systems
{
    public class DeathService
    {
        public DeathService(Board board)
        {
            Board = board;
        }

        public Board Board { get; }

        public void ProcessDeaths()
        {
            while (DoStep()) { }
        }

        /// <summary>
        /// Do cleaning step.
        /// </summary>
        /// <returns>False, if didn't notice dead minions</returns>
        private bool DoStep()
        {
            List<RemovedCard> removedCards = Board.CleanService.CleanInactiveCards().ToList();
            return removedCards.Count != 0;
        }
    }
}