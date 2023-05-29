using HsLib.Systems;
using HsLib.Types.BoardSubscribers;
using HsLib.Types.Effects;
using HsLib.Types.GameActions;
using HsLib.Types.GameIntents;
using HsLib.Types.Places;
using HsLib.Types.Stats;

namespace HsLib.Types.Cards
{
    public abstract class Minion : Card, IAttacker, IDamageable, IMortal, IWithDeathrattle, IPlayableFromHand, IWithBattlecry
    {
        protected Minion(int mp, int atk, int hp) : base(mp)
        {
            Atk = new Atk(atk);
            Hp = new Hp(hp);
        }

        public Atk Atk { get; private set; }

        public int AtksThisTurn { get; private set; }

        public Hp Hp { get; private set; }

        public BoolStat Taunt { get; private set; } = new BoolStat(false);
        public BoolStat Charge { get; private set; } = new BoolStat(false);
        public Windfury Windfury { get; private set; } = new Windfury(false);
        public BoolStat DivineShield { get; private set; } = new BoolStat(false);
        public BoolStat Stealth { get; private set; } = new BoolStat(false);

        public BattlecryEffect? BattlecryEffect { get; protected set; }

        protected List<IBoardSubscriber> FieldEffectSources { get; } = new();

        public DeathrattleEffect? DeathrattleEffect { get; protected set; }

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

        public virtual void AfterAttack(IBoard board)
        {
            AtksThisTurn++;
        }

        public override void Subscribe(IBoard board)
        {
            base.Subscribe(board);
            AtksThisTurn = 0;

            if (Place.Loc == Loc.Field)
            {
                FieldEffectSources.ForEach(e => e.Subscribe(board));
            }
        }

        public override void Unsubscribe(IBoard board, Place previousPlace)
        {
            base.Unsubscribe(board, previousPlace);
            AtksThisTurn = 0;

            if (previousPlace.Loc == Loc.Field)
            {
                FieldEffectSources.ForEach(e => e.Unsubscribe(board, previousPlace));
            }

            if (Dead) { DeathrattleEffect?.ActivateDeathrattle(board, previousPlace.Pid)(); }
        }

        protected override void OnTurnEnd()
        {
            base.OnTurnEnd();
            AtksThisTurn = 0;
        }

        public virtual bool ActivateDeathrattle(IBoard board) { return false; }

        public bool CanMeleeAttack(IBoard board)
        {
            if (Dead) { return false; }
            if (Windfury.AttacksLeft(AtksThisTurn) <= 0) { return false; }
            return !board.Turn.IsFirstTurn(AddedTurnNo) || Charge;
        }

        public bool CanBeMeleeAttacked(IBoard board)
        {
            return !Stealth && (Taunt || !board[Place.Pid].Field.HasAnyActiveTaunt());
        }

        public IDamageable GetDefender(IBoard board)
        {
            return this;
        }

        public override bool CanProcessIntent(GameIntent intent)
        {
            if (Board is null) { return false; }

            switch (intent)
            {
                case PlayFromHandIntent playFromHandIntent:
                    if (!Board[Place.Pid].Mp.IsEnough(Mp)) { return false; }

                    ICard? effectTarget = playFromHandIntent.EffectTarget;
                    return TargetableEffectValidator.IsEffectTargetValid(BattlecryEffect, Board, effectTarget);

                default:
                    return false;
            }
        }

        public override IEnumerable<GameAction>? ProcessIntent(GameIntent intent)
        {
            if (Board is null) { return null; }

            switch (intent)
            {
                case PlayFromHandIntent playFromHandIntent:
                    List<GameAction> actions = new();
                    actions.Add(new ContainerRemoveAction(this, Place));
                    int? fieldIndex = playFromHandIntent.FieldIndex;
                    if (fieldIndex is null)
                    {
                        actions.Add(new ContainerAddAction(this, Place with { Loc = Loc.Field }));
                    }
                    else
                    {
                        actions.Add(new ContainerInsertAction(this, Place with { Loc = Loc.Field }, fieldIndex.Value));
                    }
                    Board[Place.Pid].Mp.Decrease(Mp); // todo to GameAction

                    // todo refactor this:
                    //Action? battlectyAction = BattlecryEffect?.UseEffect(board, effectTarget);

                    return actions.AsEnumerable();

                default:
                    return null;
            }
        }

        public override bool ShouldBeCleaned()
        {
            return Place.Loc == Loc.Field && Dead;
        }

        public override ICard Clone()
        {
            Minion cloned = (Minion)base.Clone();

            cloned.Atk = (Atk)Atk.Clone();
            cloned.Hp = (Hp)Hp.Clone();
            cloned.Taunt = (BoolStat)Taunt.Clone();
            cloned.Charge = (BoolStat)Charge.Clone();
            cloned.Windfury = (Windfury)Windfury.Clone();
            cloned.DivineShield = (BoolStat)DivineShield.Clone();
            cloned.Stealth = (BoolStat)Stealth.Clone();

            if (cloned.BattlecryEffect is not null) { cloned.BattlecryEffect.Owner = cloned; }
            cloned.FieldEffectSources.ForEach(s => s.Owner = cloned);

            return cloned;
        }
    }
}