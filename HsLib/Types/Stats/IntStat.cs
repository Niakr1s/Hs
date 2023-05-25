namespace HsLib.Types.Stats
{
    public class IntStat : Stat<int>
    {
        public IntStat(int value) : base(value)
        {
        }

        private readonly EnchantList<int> _finalMultipliers = new();

        public event EventHandler<StatDecreasedEventArgs>? Decreased;

        public event EventHandler<StatIncreasedEventArgs>? Increased;

        public override int Value
        {
            get
            {
                int value = base.Value;
                foreach (var item in _finalMultipliers.Enchants) { value *= item.Value; }
                return value;
            }
        }

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

        public Enchant<int> AddFinalMultiplierAura(int value)
        {
            Enchant<int> aura = new Enchant<int>(value);
            _finalMultipliers.Add(aura);
            return aura;
        }

        protected override void DoReset()
        {
            base.DoReset();
            _finalMultipliers.Clear();
        }

        public override Stat<int> Clone()
        {
            Stat<int> cloned = base.Clone();
            _finalMultipliers.Clear();
            return cloned;
        }
    }
}