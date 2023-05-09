using HsLib.Battle;
using HsLib.Common.Interfaces;
using HsLib.Stats;
using HsLib.Stats.Base;

namespace HsLib.Cards
{
    public abstract class Weapon : Card, IAttacker, IMortal
    {
        protected Weapon(int mp, int atk, int hp) : base(mp)
        {
            Atk = new Atk(atk);
            Hp = new Hp(hp);
        }

        public Atk Atk { get; }

        public int AtksThisTurn { get; set; }

        public Hp Hp { get; }

        public BoolStat Charge => new BoolStat(true);
        public BoolStat Windfury { get; init; } = new BoolStat(false);

        public bool Dead => Hp.Value <= 0;

        public virtual bool ActivateDeathrattle(Battlefield bf) { return false; }

        public void AfterAttack(Battlefield bf)
        {
            Hp.Decrease();
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
    }
}
