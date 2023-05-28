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

        public virtual bool ActivateDeathrattle(Battlefield bf) { return false; }

        public void AfterAttack(Battlefield bf)
        {
            AtksThisTurn++;
            Hp.Decrease();
        }

        public override void Subscribe(Battlefield bf)
        {
            base.Subscribe(bf);
            AtksThisTurn = 0;
        }

        public override void Unsubscribe(Battlefield bf, Place previousPlace)
        {
            base.Unsubscribe(bf, previousPlace);
            AtksThisTurn = 0;
        }

        public bool CanMeleeAttack(Battlefield bf)
        {
            if (Dead) { return false; }
            return Windfury.AttacksLeft(AtksThisTurn) > 0;
        }

        public Action PlayFromHand(Battlefield bf, int? fieldIndex = null, ICard? effectTarget = null)
        {
            TargetableEffectValidator.ValidateEffectTarget(BattlecryEffect, bf, effectTarget);

            void move()
            {
                BattlefieldPlayer player = bf[Place.Pid];
                player.Hand.Remove(this);
                player.Weapon = this;
            }

            Action? battlecryAction = BattlecryEffect?.UseEffect(bf, effectTarget);

            return () =>
            {
                battlecryAction?.Invoke();
                move();
            };
        }

        public IDamageable GetDefender(Battlefield bf)
        {
            return bf[Place.Pid].Hero;
        }

        protected override void OnTurnEnd()
        {
            base.OnTurnEnd();
            AtksThisTurn = 0;
        }

        public override bool ShouldBeCleaned()
        {
            return Place.Loc == Loc.Field && Dead;
        }
    }
}
