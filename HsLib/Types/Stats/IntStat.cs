namespace HsLib.Types.Stats
{
    public class IntStat : Stat<int>
    {
        public IntStat(int value) : base(value)
        {
        }

        protected sealed override int Sum(int a1, int a2)
        {
            return a1 + a2;
        }

        protected sealed override int Sanitize(int value)
        {
            return value < 0 ? 0 : value;
        }

        public void Decrease(int value = 1)
        {
            _value -= value;
        }

        public void Increase(int value = 1)
        {
            _value += value;
        }

        public static implicit operator int(IntStat stat) => stat.Value;
    }
}