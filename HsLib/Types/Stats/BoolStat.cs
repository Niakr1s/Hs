namespace HsLib.Types.Stats
{
    public class BoolStat : Stat<bool>
    {
        public BoolStat(bool value) : base(value)
        {
        }

        public void Silence()
        {
            _value = false;
            Auras.Clear();
            Buffs.Clear();
        }

        protected sealed override bool Sum(bool a1, bool a2)
        {
            return a1 || a2;
        }

        protected sealed override bool Sanitize(bool value)
        {
            return value;
        }

        public static implicit operator bool(BoolStat stat) => stat.Value;
    }
}