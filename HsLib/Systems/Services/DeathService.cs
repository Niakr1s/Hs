using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types;
using HsLib.Types.Cards;
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
                .Where(c => c?.Dead == true && c.Loc != Loc.Graveyard).Select(c => c!).ToList();

            if (dead.Count == 0) { return false; }

            MoveInactiveCardsToGraveyard();

            foreach (IMortal m in dead)
            {
                m.ActivateDeathrattle(Bf);
            }

            return true;
        }

        private void MoveInactiveCardsToGraveyard()
        {
            foreach (Pid pid in Pids.All())
            {
                foreach (Card card in Bf[pid].CleanInactiveCards())
                {
                    Bf[pid].Graveyard.Add(card);
                }
            }
        }
    }
}