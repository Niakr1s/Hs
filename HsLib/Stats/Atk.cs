using HsLib.Stats.Base;

namespace HsLib.Stats
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

        public int AtksThisTurn { get; set; }
    }
}
