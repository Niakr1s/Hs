namespace HsLib.Types.Stats
{
    public class EnchantList<T>
    {
        public event EventHandler? EnchantAdded;
        public event EventHandler? EnchantRemoved;

        private readonly List<Enchant<T>> _enchants = new List<Enchant<T>>();
        public IEnumerable<Enchant<T>> Enchants => _enchants;

        public void Add(Enchant<T> enchant)
        {
            _enchants.Add(enchant);
            EnchantAdded?.Invoke(this, EventArgs.Empty);
            enchant.EnchantDeactivated += OnEnchantDeactivated;
        }

        private void OnEnchantDeactivated(object? sender, EventArgs e)
        {
            Enchant<T> buff = (Enchant<T>)sender!;
            buff.EnchantDeactivated -= OnEnchantDeactivated;
            _enchants.Remove(buff);
            EnchantRemoved?.Invoke(this, EventArgs.Empty);
        }

        public void Clear()
        {
            // make a copy!
            List<Enchant<T>> enchants = new(_enchants);
            enchants.ForEach(e => e.Deactivate());
        }
    }
}