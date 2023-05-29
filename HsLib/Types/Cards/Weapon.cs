using HsLib.Systems;
using HsLib.Types.Effects;
using HsLib.Types.Places;
using HsLib.Types.Stats;

namespace HsLib.Types.Cards
{
    public abstract class Weapon : Card, IAttacker, IMortal, IPlayableFromHand
    {
        protected Weapon(int mp, int atk, int hp) : base(mp)
        {
            Atk = new Atk(atk);
            Hp = new Hp(hp);
        }

        public Atk Atk { get; }

        public int AtksThisTurn { get; private set; }

        public Hp Hp { get; }

        public Windfury Windfury { get; init; } = new Windfury(false);

        public BattlecryEffect? BattlecryEffect { get; }

        public bool Dead
        {
            get
            {
                return Hp.Dead;
            }
            set
            {
                Hp.Dead = value;
            }
        }

        public virtual bool ActivateDeathrattle(IBoard board) { return false; }

        public void AfterAttack(IBoard board)
        {
            AtksThisTurn++;
            Hp.Decrease();
        }

        public override void Subscribe(IBoard board)
        {
            base.Subscribe(board);
            AtksThisTurn = 0;
        }

        public override void Unsubscribe(IBoard board, Place previousPlace)
        {
            base.Unsubscribe(board, previousPlace);
            AtksThisTurn = 0;
        }

        public bool CanMeleeAttack(IBoard board)
        {
            if (Dead) { return false; }
            return Windfury.AttacksLeft(AtksThisTurn) > 0;
        }

        public Action PlayFromHand(IBoard board, int? fieldIndex = null, ICard? effectTarget = null)
        {
            TargetableEffectValidator.ValidateEffectTarget(BattlecryEffect, board, effectTarget);

            void move()
            {
                IBoardSide player = board[Place.Pid];
                player.Hand.Remove(this);
                player.Weapon = this;
            }

            Action? battlecryAction = BattlecryEffect?.UseEffect(board, effectTarget);

            return () =>
            {
                battlecryAction?.Invoke();
                move();
            };
        }

        public IDamageable GetDefender(IBoard board)
        {
            return board[Place.Pid].Hero;
        }

        protected override void OnTurnEnd()
        {
            base.OnTurnEnd();
            AtksThisTurn = 0;
        }

        public override bool ShouldBeCleaned()
        {
            return Place.Loc == Loc.Field && Dead;
        }
    }
}
