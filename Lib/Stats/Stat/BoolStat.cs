namespace Models.Stats.Stat
{
    public class BoolStat : Stat<bool>
    {
        protected BoolStat(bool value) : base(value)
        {
        }

        public override void Silence()
        {
            _value = false;
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