using HsLib.Types.Stats;

namespace HsLibTests.Types.Stats
{
    [TestClass()]
    public class EnchantTests
    {
        [TestMethod()]
        public void EnchantTest()
        {
            Enchant<int> enchant = new(1);
            Assert.AreEqual(1, enchant.Value);
            Assert.AreEqual(true, enchant.Active);
        }

        [TestMethod()]
        public void DeactivateTest()
        {

            Enchant<int> enchant = new(1);

            int enchantDeactivatedCount = 0;
            enchant.EnchantDeactivated += (s, e) => enchantDeactivatedCount++;

            enchant.Deactivate();
            Assert.AreEqual(1, enchantDeactivatedCount);
            Assert.AreEqual(false, enchant.Active);

            for (int i = 0; i < 3; i++)
            {
                enchant.Deactivate();
                Assert.AreEqual(1, enchantDeactivatedCount);
                Assert.AreEqual(false, enchant.Active);
            }
        }
    }
}