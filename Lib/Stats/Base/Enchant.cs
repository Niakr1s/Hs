namespace Models.Stats.Base
{
    public class Enchant<T>
    {
        public Enchant(T value)
        {
            Value = value;
        }

        public T Value { get; }

        public bool Active { get; set; } = true;
    }
}