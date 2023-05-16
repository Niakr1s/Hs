using HsLib.Exceptions;
using HsLib.Interfaces;
using HsLib.Interfaces.CardTraits;
using HsLib.Systems;
using HsLib.Types.Stats;
using HsLib.Types.Stats.Base;

namespace HsLib.Types.Cards
{
    public abstract class Minion : Card, IAttacker, IDamageable, IMortal, IWithDeathrattle, IWithBattlecry, IWithChoseOne, IPlayableFromHand
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

        public IActiveEffect? Battlecry { get; protected set; }

        public IActiveEffect? Deathrattle { get; protected set; }

        public bool Dead => Hp <= 0;

        public virtual IEnumerable<CardId>? ChoseOne { get; }

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
            base.OnTurnEnd(bf);
            AtksThisTurn = 0;
        }

        public virtual bool ActivateDeathrattle(Battlefield bf) { return false; }

        public bool CanMeleeAttack(Battlefield bf)
        {
            if (Dead) { return false; }
            if (Windfury.AttacksLeft(AtksThisTurn) <= 0) { return false; }
            return !bf.Turn.IsFirstTurn(Place!.AddedTurnNo) || Charge;
        }

        public bool CanBeMeleeAttacked(Battlefield bf)
        {
            return !Stealth && (Taunt || !bf[Place!.Pid].Field.HasAnyActiveTaunt());
        }

        public IDamageable GetDefender(Battlefield bf)
        {
            return this;
        }

        public Action PlayFromHand(Battlefield bf, int? fieldIndex = null, ICard? effectTarget = null)
        {
            ValidateEffectTarget(bf, Place!.Pid, Battlecry, effectTarget, isSpell: false);

            Minion transformTo = ChoseOne is null ? this : (Minion)CardBuilder.FromId(bf[Place!.Pid].Player.ChooseOne(ChoseOne));

            Action move = bf[Place!.Pid].Hand.MoveToContainer(Place.Index, bf[Place.Pid].Field,
                canBurn: false, toIndex: fieldIndex, transformTo: transformTo);

            Action? battlectyAction = Battlecry?.UseEffect(bf, Place.Pid, effectTarget);

            return () =>
            {
                battlectyAction?.Invoke();
                move();
            };
        }

        // todo: make separate class and make tests
        private static void ValidateEffectTarget(Battlefield bf, Pid pid, IActiveEffect? playFromHandEffect, ICard? effectTarget,
            bool isSpell)
        {
            if (playFromHandEffect is not null)
            {
                if (!playFromHandEffect.GetPossibleTargets(bf, pid).Any())
                {
                    if (isSpell && effectTarget is null)
                    {
                        throw new ValidationException("effect target is null even though it never must be not");
                    }
                }

                if (effectTarget is null)
                {
                    if (playFromHandEffect.GetPossibleTargets(bf, pid).Any())
                    {
                        throw new ValidationException("effect target is null even though possible targets are present");
                    }
                }
                else
                {
                    if (!playFromHandEffect.GetPossibleTargets(bf, pid).Contains(effectTarget))
                    {
                        throw new ValidationException(
                        "effect target is not null even though no possible targets are present");
                    }
                }
            }
            else
            {
                if (effectTarget is not null)
                {
                    throw new ValidationException(
                    "effect target is not null even though battlecry is null");
                }
            }
        }
    }
}
