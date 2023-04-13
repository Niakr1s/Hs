namespace Models.Stats.Base
{
    public class BoolStat : Stat<bool>
    {
        protected BoolStat(bool value) : base(value)
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
    }
}