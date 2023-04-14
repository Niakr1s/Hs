using Models.Cards.Effects;
using Models.Services.Battle;
using Models.Stats;

namespace Models.Cards
{
    public abstract class Minion : Card, IAttacker, IDamageable
    {
        protected Minion(int mp, int atk, int hp) : base(mp)
        {
            Atk = new Atk(atk);
            Hp = new Hp(hp);
        }

        public Atk Atk { get; }

        public Hp Hp { get; }

        public Battlecry? Battlecry { get; protected set; }

        public bool Dead => Hp.Value <= 0;

        public int GetDamage(int value)
        {
            return Hp.GetDamage(value);
        }
    }
}
