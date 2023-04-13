using System.Collections;

namespace Models.Stats.Enchant
{
    public class EnchantList<T>
    {
        private List<IEnchant<T>> _enchants = new List<IEnchant<T>>();
        public IEnumerable Enchants => _enchants;

        public void Add(IEnchant<T> enchant)
        {
            _enchants.Add(enchant);
        }

        public void ClearInactiveEnchants()
        {
            _enchants = _enchants.Where(a => a.Active).ToList();
        }

        public void Clear()
        {
            _enchants.Clear();
        }
    }
}