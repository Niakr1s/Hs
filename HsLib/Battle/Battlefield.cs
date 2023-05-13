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

        /// <summary>
        /// Gets player.
        /// </summary>
        /// <param name="pid"></param>
        public BattlefieldPlayer this[Pid pid] => _bf[pid];

        /// <summary>
        /// Current player.
        /// </summary>
        public BattlefieldPlayer Player => this[Turn.Pid];

        /// <summary>
        /// Current enemy.
        /// </summary>
        public BattlefieldPlayer Enemy => this[Turn.Pid.He()];

        #region MeleeAttack

        public bool WeaponAttack(Loc defenderLoc, int defenderIndex)
        {
            return BattleService.MeleeAttack(Player.Weapon.Card, GetDefender(defenderLoc, defenderIndex), Player.Hero.Card);
        }

        public bool WeaponAttack(IDamageable defender)
        {
            if (defender.Pid == Turn.Pid) { throw new PidException(); }
            return WeaponAttack(defender.Loc, defender.Index);
        }

        public bool MinionAttack(int attackerIndex, Loc defenderLoc, int defenderIndex)
        {
            Minion attacker = (Minion)Player.GetCard(Loc.Field, attackerIndex);
            IDamageable defender = GetDefender(defenderLoc, defenderIndex);
            return MinionAttack(attacker, defender);
        }

        public bool MinionAttack(Minion attacker, IDamageable defender)
        {
            if (attacker.Pid != Player.Pid) { throw new PidException(); }
            if (attacker.Loc != Loc.Field) { throw new LocException(); }
            if (defender.Pid != Enemy.Pid) { throw new PidException(); }
            if (defender.Loc != Loc.Field && defender.Loc != Loc.Hero) { throw new LocException(); }
            return BattleService.MeleeAttack(attacker, defender);
        }

        private IDamageable GetDefender(Loc loc, int index)
        {
            if (loc != Loc.Hero && loc != Loc.Field)
            {
                throw new LocException();
            }

            return Enemy.GetCard(loc, index) as IDamageable ?? throw new ArgumentException("not damageable");
        }

        #endregion

        #region PlayFromHand

        public bool PlayFromHand(int index, int? fieldIndex = null)
        {
            // TODO
            return true;
        }

        #endregion

        #region UseAbility

        public bool UseAbility(Pid targetPid, Loc targetLoc, int targetIndex)
        {
            return UseAbility(this[targetPid].GetCard(targetLoc, targetIndex));
        }

        public bool UseAbility(Card target)
        {
            return BattleService.UseAbility(Turn.Pid, target);
        }

        public bool UseAbility()
        {
            return BattleService.UseAbility(Turn.Pid);
        }

        #endregion
    }
}
