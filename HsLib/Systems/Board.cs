using HsLib.Types.Cards;
using HsLib.Types.Containers;
using HsLib.Types.Places;
using HsLib.Types.Turns;
using System.Collections.Specialized;

namespace HsLib.Systems
{
    public partial class Board : INotifyCollectionChanged
    {
        public Board(StartingDeck p1, StartingDeck p2)
        {
            Turn = new Turn();

            _board = new()
            {
                [Pid.P1] = new(this, Pid.P1, p1),
                [Pid.P2] = new(this, Pid.P2, p2),
            };

            _cards = this[Pid.P1].Cards.Concat(this[Pid.P2].Cards).ToList();

            DeathService = new DeathService(this);
            PlayerService = new PlayerService(this);
            MeleeService = new MeleeService(this);
            CleanService = new CleanService(this);

            ConnectEvents();
        }

        public Board(CardId p1, CardId p2) : this(new StartingDeck(p1), new StartingDeck(p2))
        {
        }

        public event EventHandler<TurnEventArgs>? TurnEvent
        {
            add { Turn.Event += value; }
            remove { Turn.Event -= value; }
        }

        public event NotifyCollectionChangedEventHandler? CollectionChanged
        {
            add
            {
                Array.ForEach(Pids.All(), p => this[p].CollectionChanged += value);
            }
            remove
            {
                Array.ForEach(Pids.All(), p => this[p].CollectionChanged -= value);
            }
        }

        public Turn Turn { get; }

        #region Services
        internal DeathService DeathService { get; }
        internal PlayerService PlayerService { get; }
        internal MeleeService MeleeService { get; }
        internal CleanService CleanService { get; }
        #endregion

        /// <summary>
        /// Get all cards in all containers in chronological order.
        /// </summary>
        private readonly List<ICard> _cards;
        public IEnumerable<ICard> Cards => _cards.AsEnumerable();

        private readonly Dictionary<Pid, BoardSide> _board;

        /// <summary>
        /// Gets player.
        /// </summary>
        /// <param name="pid"></param>
        public BoardSide this[Pid pid] => _board[pid];

        /// <summary>
        /// Current player.
        /// </summary>
        public BoardSide Player => this[Turn.Pid];

        /// <summary>
        /// Current enemy.
        /// </summary>
        public BoardSide Enemy => this[Turn.Pid.He()];

        public IContainer this[Place place] => this[place.Pid][place.Loc];



        #region MeleeAttack

        public bool WeaponAttack(Loc defenderLoc, int defenderIndex = 0)
        {
            Action attackAction;
            try
            {
                attackAction = MeleeService.WeaponAttack(GetDefender(defenderLoc, defenderIndex));
            }
            catch
            {
                return false;
            }

            attackAction();
            return true;
        }

        public bool MinionAttack(int attackerIndex, Loc defenderLoc, int defenderIndex)
        {
            Action attackAction;
            try
            {

                Minion attacker = Player.Field[attackerIndex];
                IDamageable defender = GetDefender(defenderLoc, defenderIndex);
                attackAction = MeleeService.MinionAttack(attacker, defender);
            }
            catch
            {
                return false;
            }

            attackAction();
            return true;
        }

        private IDamageable GetDefender(Loc loc, int index)
        {
            if (loc != Loc.Hero && loc != Loc.Field)
            {
                throw new ValidationException("wrong loc");
            }

            return Enemy[loc][index] as IDamageable ?? throw new ArgumentException("not damageable");
        }

        #endregion



        #region PlayFromHand

        /// <summary>
        /// Plays a card from hand. If something unexpected occured, throws exception.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="fieldIndex"></param>
        /// <param name="effectTarget"></param>
        /// <returns>True, if actually was played, false - if can't play from hand.</returns>
        public bool PlayFromHand(int index, int? fieldIndex = null, ICard? effectTarget = null)
        {
            Action playFromHandAction;

            // do checks
            try
            {
                playFromHandAction = Player.Hand.PlayFromHand(index, fieldIndex, effectTarget);
            }
            catch
            {
                return false;
            }

            // do actual play
            playFromHandAction();
            return true;
        }

        #endregion



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
            ICard? target = this[targetPid][targetLoc][targetIndex] as ICard;
            return UseAbility(target);
        }

        private bool UseAbility(ICard? target)
        {
            Action abilityAction;

            try
            {
                abilityAction = Player.Ability.UseAbility(this, target);
            }
            catch
            {
                return false;
            }

            abilityAction();
            return true;
        }

        #endregion
    }
}
