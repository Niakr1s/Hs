using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types.Stats;
using HsLib.Types.Stats.Base;

namespace HsLib.Types.Cards
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

        public Battlecry? Battlecry { get; protected set; }

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
            if (Dead) { return false; }
            return Windfury.AttacksLeft(AtksThisTurn) > 0;
        }

        public IDamageable GetDefender(Battlefield bf)
        {
            return bf[Pid].Hero.Card;
        }

        public override void OnTurnEnd(Battlefield bf)
        {
            AtksThisTurn = 0;
        }

        public override void PlayFromHand(Battlefield bf, int? fieldIndex = null, Card? effectTarget = null)
        {
            base.PlayFromHand(bf);
            if (Battlecry is not null) { bf.BattleService.UseEffect(Battlecry, effectTarget); }
            bf.MoveService.MoveHandToWeapon(Pid, Index);
        }
    }
}
