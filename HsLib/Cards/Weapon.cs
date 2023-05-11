using HsLib.Battle;
using HsLib.Common.MeleeAttack;
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

        public int AtksThisTurn { get; private set; }

        public Hp Hp { get; }

        public Windfury Windfury { get; init; } = new Windfury(false);

        public bool Dead => Hp.Value <= 0;

        public virtual bool ActivateDeathrattle(Battlefield bf) { return false; }

        public void AfterAttack(Battlefield bf)
        {
            AtksThisTurn++;
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

        public bool CanMeleeAttack(Battlefield bf)
        {
            if (bf[Pid].Hero.Card.Dead) { return false; }
            if (Atk == 0) { return false; }
            if (Loc != Common.Place.Loc.Field) { return false; }
            if (!bf.Turn.IsActivePid(Pid)) { return false; }
            if (Windfury.AttacksLeft(AtksThisTurn) <= 0) { return false; }
            return true;
        }

        public override void OnTurnEnd(Battlefield bf)
        {
            AtksThisTurn = 0;
        }
    }
}
