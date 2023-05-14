﻿using HsLib.Interfaces;
using HsLib.Systems.Services;
using HsLib.Types;
using HsLib.Types.Cards;
using HsLib.Types.Events;

namespace HsLib.Systems
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
            PlayerService = new PlayerService(this);

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
        internal PlayerService PlayerService { get; }
        #endregion

        /// <summary>
        /// Get all cards in all containers (not ordered).
        /// </summary>
        public IEnumerable<ICard> Cards => this[Pid.P1].Cards.Concat(this[Pid.P2].Cards);

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
            return BattleService.MeleeAttack(Player.Weapon.Card, GetDefender(defenderLoc, defenderIndex));
        }

        public bool WeaponAttack(IDamageable defender)
        {
            if (defender.Place is null) { throw new PlaceException(); }
            if (defender.Place.Pid == Turn.Pid) { throw new PidException(); }
            return WeaponAttack(defender.Place.Loc, defender.Place.Index);
        }

        public bool MinionAttack(int attackerIndex, Loc defenderLoc, int defenderIndex)
        {
            Minion attacker = (Minion)Player.GetCard(Loc.Field, attackerIndex);
            IDamageable defender = GetDefender(defenderLoc, defenderIndex);
            return MinionAttack(attacker, defender);
        }

        public bool MinionAttack(Minion attacker, IDamageable defender)
        {
            if (attacker.Place is null || defender.Place is null) { return false; }
            if (attacker.Place.Pid != Player.Pid) { throw new PidException(); }
            if (attacker.Place.Loc != Loc.Field) { throw new LocException(); }
            if (defender.Place.Pid != Enemy.Pid) { throw new PidException(); }
            if (defender.Place.Loc != Loc.Field && defender.Place.Loc != Loc.Hero) { throw new LocException(); }
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

        public bool PlayFromHand(int index, int? fieldIndex = null, ICard? effectTarget = null)
        {
            try
            {

                Player.Hand.Play(index, fieldIndex, effectTarget);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #region UseAbility

        public bool UseAbility()
        {
            return UseAbility(null);
        }

        /// <summary>
        /// Uses ability.
        /// </summary>
        /// <param name="targetPid"></param>
        /// <param name="targetLoc"></param>
        /// <param name="targetIndex">defaults to 0, to use on single containers</param>
        /// <returns></returns>
        public bool UseAbility(Pid targetPid, Loc targetLoc, int targetIndex = 0)
        {
            ICard target = this[targetPid].GetCard(targetLoc, targetIndex);
            return UseAbility(target);
        }

        private bool UseAbility(ICard? target)
        {
            try
            {
                return Player.Ability.UseAbility(target);
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}
