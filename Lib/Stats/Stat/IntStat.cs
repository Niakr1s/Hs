namespace Models.Stats.Stat
{
    public class IntStat : Stat<int>
    {
        protected IntStat(int value) : base(value)
        {
        }

        public override void Silence()
        {
            if (_value > _initialValue)
            {
                _value = _initialValue;
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
    }
}