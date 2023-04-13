using Models.Stats.Enchant;

namespace Models.Stats.Stat
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

                foreach (IEnchant<T> aura in Buffs.Enchants)
                {
                    resultValue = aura.Apply(resultValue);
                    resultValue = Sanitize(resultValue);
                }

                foreach (IEnchant<T> aura in Auras.Enchants)
                {
                    resultValue = aura.Apply(resultValue);
                }

                return Sanitize(resultValue);
            }
        }

        public static implicit operator T(Stat<T> stat) => stat.Value;

        /// <summary>
        /// Buffs can be silenced.
        /// </summary>
        public EnchantList<T> Buffs { get; } = new();

        /// <summary>
        /// Auras cannot be silenced.
        /// </summary>
        public EnchantList<T> Auras { get; } = new();


        /// <summary>
        /// Sets value. Doesn't clean auras.
        /// </summary>
        /// <param name="value"></param>
        public void Set(T value)
        {
            _value = value;
        }

        /// <summary>
        /// Resets value to initial state. Clears all auras
        /// </summary>
        public void Reset()
        {
            Buffs.Clear();
            Auras.Clear();
            _value = _initialValue;
        }

        /// <summary>
        /// Simply silences stat. Removes all buffs.
        /// </summary>
        public abstract void Silence();

        protected abstract T Sum(T a1, T a2);

        /// <summary>
        /// Function, that calls after each ApplyBuff. Used mostly by sanitize negative values. (I.e if Hp < 0, make Hp = 0)
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Value after sanitizing</returns>
        protected abstract T Sanitize(T value);
    }
}