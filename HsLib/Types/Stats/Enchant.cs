namespace HsLib.Types.Stats
{
    public class Enchant<T> : IEnchantHandler
    {

        public Enchant(T value)
        {
            Value = value;
        }

        public event EventHandler? EnchantDeactivated;

        public T Value { get; }

        public bool Active { get; private set; } = true;

        public bool Deactivate()
        {
            if (!Active) { return false; }
            Active = false;
            EnchantDeactivated?.Invoke(this, EventArgs.Empty);
            return true;
        }
    }
}