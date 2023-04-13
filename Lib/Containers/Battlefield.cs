using Models.Cards;
using Models.Common;
using Models.Events;

namespace Models.Containers
{
    public class Battlefield
    {
        public Battlefield(StartingDeck p1, StartingDeck p2)
        {
            _bf = new()
            {
                [Pid.P1] = new(this, Pid.P1, p1),
                [Pid.P2] = new(this, Pid.P2, p2),
            };
            Event += UpdateCards;
        }

        public event EventHandler<EventArgs>? Event;

        public void Invoke(object? sender, EventArgs args)
        {
            Event?.Invoke(sender, args);
        }

        /// <summary>
        /// All cards in all containers in their add order.
        /// </summary>
        /// <returns></returns>
        public List<Card> Cards { get; } = new List<Card>();

        /// <summary>
        /// Used to update <see cref="Cards"/>.
        /// </summary>
        private void UpdateCards(object? sender, EventArgs args)
        {
            if (args is ContainerEventArgs)
            {
                switch (args)
                {
                    case ContainerCardAddedEventArgs e:
                        Cards.Add(e.Card);
                        break;

                    case ContainerCardRemovedEventArgs e:
                        Cards.Remove(e.Card);
                        break;
                }
            }
        }

        private readonly Dictionary<Pid, BattlefieldSide> _bf;

        public BattlefieldSide this[Pid pid]
        {
            get => _bf[pid];
        }
    }
}
