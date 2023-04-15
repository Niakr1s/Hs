using Models.Cards;
using Models.Common;
using Models.Common.Place;
using Models.Events;
using Models.Services.Battle;

namespace Models.Containers
{
    public class Battlefield : IWithEvent<BattlefieldEventArgs>
    {
        public Battlefield(StartingDeck p1, StartingDeck p2)
        {
            Turn = new Turn();

            _bf = new()
            {
                [Pid.P1] = new(this, Pid.P1, p1),
                [Pid.P2] = new(this, Pid.P2, p2),
            };
            Event += UpdateCards;
            BattleService = new BattleService(this);
        }

        public Battlefield(HeroId p1, HeroId p2) : this(new StartingDeck(p1), new StartingDeck(p2))
        {
        }

        public event EventHandler<BattlefieldEventArgs>? Event;

        public void Invoke(object? sender, BattlefieldEventArgs args)
        {
            Event?.Invoke(sender, args);
        }

        public Turn Turn { get; }

        #region Services
        public BattleService BattleService { get; }
        #endregion

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
