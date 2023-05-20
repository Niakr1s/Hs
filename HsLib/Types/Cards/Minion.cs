using HsLib.Systems;
using HsLib.Types.Effects;
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

        public Atk Atk { get; }

        public int AtksThisTurn { get; private set; }

        public Hp Hp { get; }

        public BoolStat Taunt { get; init; } = new BoolStat(false);
        public BoolStat Charge { get; init; } = new BoolStat(false);
        public Windfury Windfury { get; init; } = new Windfury(false);
        public BoolStat DivineShield { get; init; } = new BoolStat(false);
        public BoolStat Stealth { get; init; } = new BoolStat(false);

        public BattlecryEffect? BattlecryEffect { get; protected set; }

        protected List<IBattlefieldSubscriber> FieldEffectSources { get; } = new();

        public DeathrattleEffect? DeathrattleEffect { get; protected set; }

        public bool Dead => Hp <= 0;

        public virtual IEnumerable<CardId>? ChoseOne { get; }

        public virtual void AfterAttack(Battlefield bf)
        {
            AtksThisTurn++;
        }

        public override void Subscribe(Battlefield bf)
        {
            base.Subscribe(bf);
            AtksThisTurn = 0;

            if (PlaceInContainer!.Loc == Loc.Field)
            {
                FieldEffectSources.ForEach(e => e.Subscribe(bf));
            }
        }

        public override void Unsubscribe(Battlefield bf, Place previousPlace)
        {
            base.Unsubscribe(bf, previousPlace);
            AtksThisTurn = 0;

            if (previousPlace.Loc == Loc.Field)
            {
                FieldEffectSources.ForEach(e => e.Unsubscribe(bf, previousPlace));
            }

            if (Dead) { DeathrattleEffect?.ActivateDeathrattle(bf, previousPlace.Pid)(); }
        }

        public override void OnTurnEnd(Battlefield bf)
        {
            base.OnTurnEnd(bf);
            AtksThisTurn = 0;
        }

        public virtual bool ActivateDeathrattle(Battlefield bf) { return false; }

        public bool CanMeleeAttack(Battlefield bf)
        {
            if (Dead) { return false; }
            if (Windfury.AttacksLeft(AtksThisTurn) <= 0) { return false; }
            return !bf.Turn.IsFirstTurn(PlaceInContainer!.AddedTurnNo) || Charge;
        }

        public bool CanBeMeleeAttacked(Battlefield bf)
        {
            return !Stealth && (Taunt || !bf[PlaceInContainer!.Pid].Field.HasAnyActiveTaunt());
        }

        public IDamageable GetDefender(Battlefield bf)
        {
            return this;
        }

        public Action PlayFromHand(Battlefield bf, int? fieldIndex = null, ICard? effectTarget = null)
        {
            Minion transformTo = ChoseOne is null ? this : (Minion)CardBuilder.FromId(bf[PlaceInContainer!.Pid].Player.ChooseOne(ChoseOne));

            Action move = bf[PlaceInContainer!.Pid].Hand.MoveToContainer(PlaceInContainer.Index, bf[PlaceInContainer.Pid].Field,
                canBurn: false, toIndex: fieldIndex, transformTo: transformTo);

            TargetableEffectValidator.ValidateEffectTarget(BattlecryEffect, bf, PlaceInContainer!.Pid, effectTarget);
            Action? battlectyAction = BattlecryEffect?.UseEffect(bf, PlaceInContainer!.Pid, effectTarget);

            return () =>
            {
                battlectyAction?.Invoke();
                move();
            };
        }

        public override bool ShouldBeRemovedFromCurrentContainer()
        {
            return PlaceInContainer!.Loc == Loc.Field && Dead;
        }
    }
}
