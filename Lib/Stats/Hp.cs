using Models.Stats.Base;

namespace Models.Stats
{
    public class Hp : IntStat
    {
        public Hp(int value) : base(value)
        {
        }

        public void Silence()
        {
            int hpWithEnchants = Value;
            Auras.Clear();
            Buffs.Clear();
            _value = hpWithEnchants > _initialValue ? _initialValue : hpWithEnchants;
        }
    }
}
