using HsLib.Types.Containers;

namespace HsLib.Systems
{
    public class DeathService
    {
        public DeathService(Battlefield bf)
        {
            Bf = bf;
        }

        public Battlefield Bf { get; }

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
            List<RemovedCard> removedCards = Bf.CleanService.CleanInactiveCards().ToList();
            return removedCards.Count != 0;
        }
    }
}