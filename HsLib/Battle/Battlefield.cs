using HsLib.Battle.Services;
using HsLib.Cards;
using HsLib.Common;
using HsLib.Common.MeleeAttack;
using HsLib.Common.Place;
using HsLib.Containers;
using HsLib.Events;

namespace HsLib.Battle
{
    public partial class Battlefield : IWithEvent<BattlefieldEventArgs>
    {
        public Battlefield(StartingDeck p1, StartingDeck p2)
        {
            Turn = new Turn();

            _bf = new()
            {
                [Pid.P1] = new(this, Pid.P1, p1),
                [Pid.P2] = new(this, Pid.P2, p2),
            };
            BattleService = new BattleService(this);
            DeathService = new DeathService(this);

            new EventConnector(this).Connect();
        }

        public Battlefield(CardId p1, CardId p2) : this(new StartingDeck(p1), new StartingDeck(p2))
        {
        }

        public event EventHandler<BattlefieldEventArgs>? Event;

        private void Invoke(object? sender, EventArgs e)
        {
            Event?.Invoke(sender, new BattlefieldEventArgs(this, e));
        }

        public Turn Turn { get; }

        #region Services
        internal BattleService BattleService { get; }
        internal DeathService DeathService { get; }
        #endregion

        /// <summary>
        /// Get all cards in all containers (not ordered).
        /// </summary>
        public IEnumerable<Card> Cards => this[Pid.P1].Cards.Concat(this[Pid.P2].Cards);

        private readonly Dictionary<Pid, BattlefieldPlayer> _bf;

        public BattlefieldPlayer this[Pid pid]
        {
            get => _bf[pid];
        }

        #region Main methods

        public void WeaponAttack(Card target)
        {

        }

        public void MinionAttack(Minion from, Card target)
        {

        }

        public void PlayFromHand(Card card)
        {

        }

        public void UseAbility()
        {

        }

        #endregion
    }
}
