namespace Models.Stats.Base
{
    public class IntStat : Stat<int>
    {
        protected IntStat(int value) : base(value)
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
    }
}