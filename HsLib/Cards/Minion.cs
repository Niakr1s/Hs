using HsLib.Battle;
using HsLib.Cards.Effects;
using HsLib.Common.MeleeAttack;
using HsLib.Stats;
using HsLib.Stats.Base;

namespace HsLib.Cards
{
    public abstract class Minion : Card, IAttacker, IDamageable, IMortal
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

        public Battlecry? Battlecry { get; protected set; }

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
            if (Atk == 0) { return false; }
            if (Loc != Common.Place.Loc.Field) { return false; }
            if (!bf.Turn.IsActivePid(Pid)) { return false; }
            if (Windfury.AttacksLeft(AtksThisTurn) <= 0) { return false; }
            return !bf.Turn.IsFirstTurn(TurnAdded) || Charge;
        }

        public bool CanBeMeleeAttacked(Battlefield bf)
        {
            if (Loc != Common.Place.Loc.Field) { return false; }
            if (Dead) { return false; }
            return Stealth.Value && (Taunt.Value || !bf[Pid].Field.HasAnyActiveTaunt());
        }
    }
}
