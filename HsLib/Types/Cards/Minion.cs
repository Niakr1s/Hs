using HsLib.Systems;
using HsLib.Types.BattlefieldSubscribers;
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

        public Atk Atk { get; private set; }

        public int AtksThisTurn { get; private set; }

        public Hp Hp { get; private set; }

        public BoolStat Taunt { get; private set; } = new BoolStat(false);
        public BoolStat Charge { get; private set; } = new BoolStat(false);
        public Windfury Windfury { get; private set; } = new Windfury(false);
        public BoolStat DivineShield { get; private set; } = new BoolStat(false);
        public BoolStat Stealth { get; private set; } = new BoolStat(false);

        public BattlecryEffect? BattlecryEffect { get; protected set; }

        protected List<IBattlefieldSubscriber> FieldEffectSources { get; } = new();

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
            Action move = bf[PlaceInContainer!.Pid].Hand.MoveToContainer(PlaceInContainer.Index, bf[PlaceInContainer.Pid].Field,
                canBurn: false, toIndex: fieldIndex);

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
