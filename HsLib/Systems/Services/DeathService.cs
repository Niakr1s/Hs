using HsLib.Interfaces;
using HsLib.Types;
using HsLib.Types.Containers.Base;
using HsLib.Types.Events;

namespace HsLib.Systems.Services
{
    public class DeathService
    {
        public DeathService(Battlefield bf)
        {
            Bf = bf;
        }

        public Battlefield Bf { get; }

        public event EventHandler<BattleEventArgs>? Event;

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
            List<IMortal> dead = Bf.Cards.Select(c => c as IMortal)
                .Where(c => c?.Dead == true).Select(c => c!).ToList();

            if (dead.Count == 0) { return false; }

            List<RemovedCard> removedCards = CleanInactiveCards().ToList();
            if (removedCards.Count == 0) { return false; }

            foreach (RemovedCard removed in removedCards)
            {
                if (removed.Card is IWithDeathrattle d)
                {
                    d.Deathrattle?.UseEffect(Bf, removed.Place.Pid, null);
                }
            }

            return true;
        }

        private IEnumerable<RemovedCard> CleanInactiveCards()
        {
            return Bf[Pid.P1].CleanInactiveCards().Concat(Bf[Pid.P2].CleanInactiveCards());
        }
    }
}