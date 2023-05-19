using HsLib.Types.Stats;

namespace HsLibTests.Types.Stats
{
    [TestClass()]
    public class EnchantListTests
    {
        [TestMethod()]
        public void AddTest()
        {
            EnchantList<int> enchantList = new();

            int enchantAddedCount = 0;
            int enchantRemovedCount = 0;

            enchantList.EnchantAdded += (s, e) => enchantAddedCount++;
            enchantList.EnchantRemoved += (s, e) => enchantRemovedCount++;

            Assert.AreEqual(0, enchantList.Enchants.Count());

            Enchant<int> enchant = new(1);

            enchantList.Add(enchant);
            Assert.AreEqual(1, enchantAddedCount);
            Assert.AreEqual(0, enchantRemovedCount);

            enchant.Deactivate();
            Assert.AreEqual(1, enchantAddedCount);
            Assert.AreEqual(1, enchantRemovedCount);
        }

        [TestMethod()]
        public void ClearTest()
        {
            EnchantList<int> enchantList = new();

            int enchantAddedCount = 0;
            int enchantRemovedCount = 0;

            enchantList.EnchantAdded += (s, e) => enchantAddedCount++;
            enchantList.EnchantRemoved += (s, e) => enchantRemovedCount++;

            Assert.AreEqual(0, enchantList.Enchants.Count());

            const int n = 3;
            for (int i = 0; i < n; i++)
            {
                Enchant<int> enchant = new(1);
                enchantList.Add(enchant);
            }

            Assert.AreEqual(3, enchantList.Enchants.Count());
            Assert.AreEqual(3, enchantAddedCount);
            Assert.AreEqual(0, enchantRemovedCount);

            enchantList.Clear();
            Assert.AreEqual(0, enchantList.Enchants.Count());
            Assert.AreEqual(3, enchantAddedCount);
            Assert.AreEqual(3, enchantRemovedCount);
        }
    }
}