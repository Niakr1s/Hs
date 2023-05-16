using HsLib.Interfaces;
using HsLib.Interfaces.CardTraits;
using HsLib.Types;
using HsLib.Types.Containers.Base;
using System.Collections.Specialized;

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
            CollectionChangedRecorder recorder = new(Bf);
            recorder.Record(RemoveInactiveCards);

            List<RemovedCard> removedCards = new();
            foreach ((object? sender, NotifyCollectionChangedEventArgs e) in recorder.Recorded)
            {
                if (e.OldItems is not null)
                {
                    foreach (object? card in e.OldItems)
                    {
                        if (card is ICard c && sender is IWithPlace withPlace)
                        {
                            RemovedCard removedCard = new RemovedCard(c, withPlace.Place);
                            removedCards.Add(removedCard);
                        }
                    }
                }
            }

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

        private void RemoveInactiveCards()
        {
            Bf[Pid.P1].RemoveInactiveCards();
            Bf[Pid.P2].RemoveInactiveCards();
        }
    }
}