using HsLib.Battle;
using HsLib.Common.MeleeAttack;
using HsLib.Stats;

namespace HsLib.Cards
{
    public abstract class Hero : Card, IDamageable
    {
        protected Hero(int mp = 0, int hp = 30, int armor = 0) : base(mp)
        {
            Hp = new Hp(hp);
            Armor = new Armor(armor);
        }

        public Hp Hp { get; }

        public Armor Armor { get; }

        public bool Dead => Hp.Value <= 0;

        public bool CanBeMeleeAttacked(Battlefield bf)
        {
            if (Loc != Common.Place.Loc.Hero) { return false; }
            if (Dead) { return false; }
            return !bf[Pid].Field.HasAnyActiveTaunt();
        }

        public int GetDamage(int value)
        {
            return Hp.GetDamage(value);
        }

        /// <summary>
        /// Produces ability to set in ability container by.
        /// </summary>
        /// <returns>new ability</returns>
        public abstract Ability ProduceAbility();
    }
}
