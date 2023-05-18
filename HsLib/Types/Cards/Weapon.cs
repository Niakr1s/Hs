using HsLib.Systems;
using HsLib.Types.Effects;
using HsLib.Types.Places;
using HsLib.Types.Stats;

namespace HsLib.Types.Cards
{
    public abstract class Weapon : Card, IAttacker, IMortal, IPlayableFromHand
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

        public BattlecryEffect? BattlecryEffect { get; }

        public bool Dead => Hp <= 0;

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

        public override void AfterContainerRemove(Battlefield bf, Place previousPlace)
        {
            base.AfterContainerRemove(bf, previousPlace);
            AtksThisTurn = 0;
        }

        public bool CanMeleeAttack(Battlefield bf)
        {
            if (Dead) { return false; }
            return Windfury.AttacksLeft(AtksThisTurn) > 0;
        }

        public Action PlayFromHand(Battlefield bf, int? fieldIndex = null, ICard? effectTarget = null)
        {
            ActiveEffectValidator.ValidateEffectTarget(BattlecryEffect, bf, PlaceInContainer!.Pid, effectTarget);

            Action move = bf[PlaceInContainer!.Pid].Hand.MoveToContainer(PlaceInContainer.Index, bf[PlaceInContainer.Pid].Weapon,
                canBurn: false, toIndex: 0);

            Action? battlecryAction = BattlecryEffect?.UseEffect(bf, PlaceInContainer!.Pid, effectTarget);

            return () =>
            {
                battlecryAction?.Invoke();
                move();
            };
        }

        public IDamageable GetDefender(Battlefield bf)
        {
            return bf[PlaceInContainer!.Pid].Hero.Card;
        }

        public override void OnTurnEnd(Battlefield bf)
        {
            base.OnTurnEnd(bf);
            AtksThisTurn = 0;
        }

        public override bool ShouldBeRemovedFromCurrentContainer()
        {
            return PlaceInContainer!.Loc == Loc.Field && Dead;
        }
    }
}
