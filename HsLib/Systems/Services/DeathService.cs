using HsLib.Interfaces.CardTraits;
using HsLib.Types.Containers.Base;

namespace HsLib.Systems.Services
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
            if (removedCards.Count == 0) { return false; }

            foreach (RemovedCard removed in removedCards)
            {
                if (removed.Card is IWithDeathrattle d)
                {
                    d.Deathrattle?.UseEffect(Bf, removed.Place.Pid, null)();
                }
            }

            return true;
        }
    }
}