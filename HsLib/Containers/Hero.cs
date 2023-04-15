using HsLib.Common.Place;
using HsLib.Services.Battle;
using HsLib.Stats;

namespace HsLib.Containers
{
    public class Hero : IDamageable
    {
        public Hero(Battlefield bf, Pid pid, HeroId heroId = HeroId.Jaina)
        {
            Bf = bf;
            Pid = pid;
            HeroId = heroId;

            Mp = new Mp(0);
            Hp = new Hp(30);
            Armor = new Armor(0);
        }

        public Battlefield Bf { get; }

        public Pid Pid { get; }

        public int TurnAdded { get; set; }

        public HeroId HeroId { get; }


        public Mp Mp { get; }

        public Hp Hp { get; }

        public Armor Armor { get; }

        public Loc Loc => Loc.Hero;

        public bool Dead => Hp.Value <= 0;


        public int GetDamage(int value)
        {
            return Hp.GetDamage(value);
        }
    }

    public enum HeroId
    {
        Jaina,
        Rexxar,
    }
}
