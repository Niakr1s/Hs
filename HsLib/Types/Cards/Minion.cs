using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types.Containers;
using HsLib.Types.Stats;
using HsLib.Types.Stats.Base;


namespace HsLib.Types.Cards
{
    public abstract class Minion : Card, IAttacker, IDamageable, IMortal, IWithDeathrattle, IWithBattlecry
    {
        protected Minion(int mp, int atk, int hp) : base(mp)
        {
            Atk = new Atk(atk);
            Hp = new Hp(hp);
        }

        public Atk Atk { get; }

        public int AtksThisTurn { get; private set; }

        public Hp Hp { get; }

        public BoolStat Taunt { get; init; } = new BoolStat(false);
        public BoolStat Charge { get; init; } = new BoolStat(false);
        public Windfury Windfury { get; init; } = new Windfury(false);
        public BoolStat DivineShield { get; init; } = new BoolStat(false);
        public BoolStat Stealth { get; init; } = new BoolStat(false);

        public IEffect? Battlecry { get; protected set; }

        public IEffect? Deathrattle { get; protected set; }

        public bool Dead => Hp.Value <= 0;

        public int GetDamage(int value)
        {
            return Hp.GetDamage(value);
        }

        public virtual void AfterAttack(Battlefield bf)
        {
            AtksThisTurn++;
        }

        public override void AfterContainerInsert(Battlefield bf)
        {
            base.AfterContainerInsert(bf);
            AtksThisTurn = 0;
        }

        public override void AfterContainerRemove(Battlefield bf)
        {
            base.AfterContainerRemove(bf);
            AtksThisTurn = 0;
        }

        public override void OnTurnEnd(Battlefield bf)
        {
            AtksThisTurn = 0;
        }

        public virtual bool ActivateDeathrattle(Battlefield bf) { return false; }

        public bool CanMeleeAttack(Battlefield bf)
        {
            if (Dead) { return false; }
            if (Windfury.AttacksLeft(AtksThisTurn) <= 0) { return false; }
            return !bf.Turn.IsFirstTurn(TurnAdded) || Charge;
        }

        public bool CanBeMeleeAttacked(Battlefield bf)
        {
            if (Place is null) { throw new PlaceException(); }
            return !Stealth.Value && (Taunt.Value || !bf[Place.Pid].Field.HasAnyActiveTaunt());
        }

        public IDamageable GetDefender(Battlefield bf)
        {
            return this;
        }

        protected override void DoPlayFromHand(Battlefield bf, int? fieldIndex = null, Card? effectTarget = null)
        {
            if (Place is null) { throw new PlaceException(); }

            MoveFromHandToField(bf, fieldIndex, check: true); // checking first

            if (Battlecry is not null) { bf.BattleService.UseEffect(Battlecry, effectTarget); }

            MoveFromHandToField(bf, fieldIndex, check: false); // playing actually
        }

        private bool MoveFromHandToField(Battlefield bf, int? fieldIndex = null, bool check = false)
        {
            if (Place is null) { throw new PlaceException(); }

            Hand hand = bf[Place.Pid].Hand;
            Field field = bf[Place.Pid].Field;

            int index = fieldIndex ?? field.Count;
            if (!field.CanBeInsertedAt(index)) { throw new IndexOutOfRangeException(); }

            if (!check)
            {
                hand.RemoveAt(Place.Index);
                field.Insert(index, this);
            }

            return true;
        }
    }
}
