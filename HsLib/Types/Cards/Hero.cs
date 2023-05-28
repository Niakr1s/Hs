using HsLib.Systems;
using HsLib.Types.Places;
using HsLib.Types.Stats;


namespace HsLib.Types.Cards
{
    public abstract class Hero : Card, IDamageable, IWithArmor
    {
        protected Hero(int mp = 0, int hp = 30, int armor = 0) : base(mp)
        {
            Hp = new Hp(hp);
            Armor = new Armor(armor);
        }

        public Hp Hp { get; }

        public Armor Armor { get; }

        public bool Dead => Hp <= 0;

        public bool CanBeMeleeAttacked(Board board)
        {
            if (Place.Loc != Loc.Hero) { return false; }
            if (Dead) { return false; }
            return !board[Place.Pid].Field.HasAnyActiveTaunt();
        }

        /// <summary>
        /// Produces ability to set in ability container by.
        /// </summary>
        /// <returns>new ability</returns>
        public abstract Ability ProduceAbility();
    }
}
