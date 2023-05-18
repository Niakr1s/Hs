namespace HsLib.Types.Stats
{
    public class Enchant<T> : IEnchantHandler
    {
        public Enchant(T value)
        {
            Value = value;
        }

        public T Value { get; }

        public bool Active { get; set; } = true;
    }
}