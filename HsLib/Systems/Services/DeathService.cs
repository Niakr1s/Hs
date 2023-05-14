using HsLib.Interfaces;
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
                .Where(c => c?.Dead == true).Select(c => c!).ToList();

            if (dead.Count == 0) { return false; }

            List<Card> cleanedCards = CleanInactiveCards().ToList();
            if (cleanedCards.Count == 0) { return false; }

            foreach (Card card in cleanedCards)
            {
                if (card is IWithDeathrattle d)
                {
                    // TODO: Place refactor
                    d.Deathrattle?.UseEffect(Bf, null);
                }
            }

            return true;
        }

        private IEnumerable<Card> CleanInactiveCards()
        {
            return Bf[Pid.P1].CleanInactiveCards().Concat(Bf[Pid.P2].CleanInactiveCards());
        }
    }
}