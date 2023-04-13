using Models.Stats;

namespace Models.Cards
{
    public abstract class Minion : Card
    {
        protected Minion(int mp, int atk, int hp) : base(mp)
        {
            Atk = new Atk(atk);
            Hp = new Hp(hp);
        }

        public Atk Atk { get; }

        public Hp Hp { get; }
    }
}
