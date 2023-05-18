using System.Collections;

namespace HsLib.Types.Stats
{
    public class EnchantList<T>
    {
        private List<Enchant<T>> _enchants = new List<Enchant<T>>();
        public IEnumerable Enchants => _enchants;

        public void Add(Enchant<T> enchant)
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

        public void AddRange(EnchantList<T> other)
        {
            _enchants.AddRange(other._enchants);
        }
    }
}