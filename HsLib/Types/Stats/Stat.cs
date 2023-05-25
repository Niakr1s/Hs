using Force.DeepCloner;

namespace HsLib.Types.Stats
{
    public abstract class Stat<T> : IStat
        where T : struct
    {
        protected Stat(T value)
        {
            _initialValue = value;
            _value = value;

            Buffs = new();
            Auras = new();

            Buffs.EnchantAdded += OnBuffAdded;
            Buffs.EnchantRemoved += OnBuffRemoved;

            Auras.EnchantAdded += OnAuraAdded;
            Auras.EnchantRemoved += OnAuraRemoved;
        }



        private void OnBuffAdded(object? sender, EventArgs e)
        {
            EmitStatChanged(StatChangedEventType.BuffAdded);
        }

        private void OnBuffRemoved(object? sender, EventArgs e)
        {
            EmitStatChanged(StatChangedEventType.BuffRemoved);
        }

        private void OnAuraAdded(object? sender, EventArgs e)
        {
            EmitStatChanged(StatChangedEventType.AuraAdded);
        }

        private void OnAuraRemoved(object? sender, EventArgs e)
        {
            EmitStatChanged(StatChangedEventType.AuraRemoved);
        }



        protected readonly T _initialValue;

        protected T _value;
        public virtual T Value
        {
            get
            {
                T resultValue = _value;

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

        public event EventHandler<StatChangedEventArgs>? StatChanged;

        /// <summary>
        /// Buffs can be silenced.
        /// </summary>
        protected EnchantList<T> Buffs { get; }

        /// <summary>
        /// Auras cannot be silenced.
        /// </summary>
        protected EnchantList<T> Auras { get; }

        public Enchant<T> AddBuff(T value)
        {
            Enchant<T> buff = new Enchant<T>(value);
            Buffs.Add(buff);
            return buff;
        }

        public Enchant<T> AddAura(T value)
        {
            Enchant<T> aura = new Enchant<T>(value);
            Auras.Add(aura);
            return aura;
        }

        /// <summary>
        /// Sets value. Doesn't clean auras. Cleans buffs.
        /// </summary>
        /// <param name="value"></param>
        public void Set(T value)
        {
            Buffs.Clear();

            _value = value;
            EmitStatChanged(StatChangedEventType.ValueSet);
        }

        /// <summary>
        /// Resets value to initial state. Clears all auras and buffs.
        /// </summary>
        public void Reset()
        {
            Buffs.Clear();
            Auras.Clear();

            _value = _initialValue;
            DoReset();

            EmitStatChanged(StatChangedEventType.Reset);
        }

        protected virtual void DoReset() { }

        private void EmitStatChanged(StatChangedEventType type)
        {
            StatChanged?.Invoke(this, new StatChangedEventArgs(type));
        }

        protected abstract T Sum(T a1, T a2);

        /// <summary>
        /// Function, that calls after each ApplyBuff. Used mostly by sanitize negative values. (I.e if Hp < 0, make Hp = 0)
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Value after sanitizing</returns>
        protected abstract T Sanitize(T value);

        public virtual Stat<T> Clone()
        {
            Stat<T> cloned = this.DeepClone();
            cloned.Auras.Clear();
            return cloned;
        }
    }
}