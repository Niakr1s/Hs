using HsLib.Battle;
using HsLib.Cards.Effects;
using HsLib.Common.Interfaces;
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

        public int AtksThisTurn { get; set; }

        public Hp Hp { get; }

        public BoolStat Taunt { get; init; } = new BoolStat(false);
        public BoolStat Charge { get; init; } = new BoolStat(false);
        public BoolStat Windfury { get; init; } = new BoolStat(false);
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
    }
}
