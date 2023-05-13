using HsLib.Types.Stats.Base;

namespace HsLib.Types.Stats
{
    public class Atk : IntStat
    {
        public Atk(int value) : base(value)
        {
        }

        public void Silence()
        {
            Auras.Clear();
            Buffs.Clear();
            _value = _initialValue;
        }
    }
}
