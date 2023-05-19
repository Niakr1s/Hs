namespace HsLib.Types.Stats
{
    public class IntStat : Stat<int>
    {
        public IntStat(int value) : base(value)
        {
        }

        public event EventHandler<StatDecreasedEventArgs>? Decreased;

        public event EventHandler<StatIncreasedEventArgs>? Increased;

        protected sealed override int Sum(int a1, int a2)
        {
            return a1 + a2;
        }

        protected sealed override int Sanitize(int value)
        {
            return value < 0 ? 0 : value;
        }

        /// <summary>
        /// Decreases <see cref="Stat{T}._value"/> directly.
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="ArgumentException"></exception>
        public virtual void Decrease(int value = 1)
        {
            if (value < 0) { throw new ArgumentException("value shouldn't be negative"); }
            Decreased?.Invoke(this, new(value));
            _value -= value;
        }

        /// <summary>
        /// Increases <see cref="Stat{T}._value"/> directly.
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="ArgumentException"></exception>
        public virtual void Increase(int value = 1)
        {
            if (value < 0) { throw new ArgumentException("value shouldn't be negative"); }
            Increased?.Invoke(this, new(value));
            _value += value;
        }

        public static implicit operator int(IntStat stat) => stat.Value;
    }
}