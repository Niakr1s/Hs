namespace HsLib.Types.Stats.Base
{
    public abstract class Stat<T>
        where T : struct
    {
        protected Stat(T value)
        {
            _initialValue = value;
            _value = value;
        }

        protected readonly T _initialValue;

        protected T _value;
        public T Value
        {
            get
            {
                T resultValue = _value;

                Auras.ClearInactiveEnchants();
                Buffs.ClearInactiveEnchants();

                foreach (Enchant<T> buff in Buffs.Enchants)
                {
                    resultValue = Sum(buff.Value, resultValue);
                    resultValue = Sanitize(resultValue);
                }

                foreach (Enchant<T> aura in Auras.Enchants)
                {
                    resultValue = Sum(aura.Value, resultValue);
                }

                return Sanitize(resultValue);
            }
        }

        public static implicit operator T(Stat<T> stat) => stat.Value;

        /// <summary>
        /// Buffs can be silenced.
        /// </summary>
        protected EnchantList<T> Buffs { get; } = new();

        /// <summary>
        /// Auras cannot be silenced.
        /// </summary>
        protected EnchantList<T> Auras { get; } = new();

        public Enchant<T> AddBuff(T value)
        {
            Enchant<T> enchant = new Enchant<T>(value);
            Buffs.Add(enchant);
            return enchant;
        }

        public Enchant<T> AddAura(T value)
        {
            Enchant<T> enchant = new Enchant<T>(value);
            Auras.Add(enchant);
            return enchant;
        }

        /// <summary>
        /// Sets value. Doesn't clean auras. Cleans buffs.
        /// </summary>
        /// <param name="value"></param>
        public void Set(T value)
        {
            Buffs.Clear();
            _value = value;
        }

        /// <summary>
        /// Resets value to initial state. Clears all auras and buffs.
        /// </summary>
        public void Reset()
        {
            Buffs.Clear();
            Auras.Clear();
            _value = _initialValue;
        }

        public void CopyBuffs(Stat<T> from)
        {
            Buffs.AddRange(from.Buffs);
        }

        protected abstract T Sum(T a1, T a2);

        /// <summary>
        /// Function, that calls after each ApplyBuff. Used mostly by sanitize negative values. (I.e if Hp < 0, make Hp = 0)
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Value after sanitizing</returns>
        protected abstract T Sanitize(T value);
    }
}