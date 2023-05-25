using HsLib.KnownCards.Minions;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Containers;

namespace HsLibTests.Types.Cards
{
    [TestClass()]
    public class MinionTests
    {
        [TestMethod()]
        public void CloneTest_StatsClonedProperly()
        {
            CloneTestHelpers.DoStatTest<int>(m => m.Atk);
            CloneTestHelpers.DoStatTest<int>(m => m.Hp);
            CloneTestHelpers.DoStatTest<bool>(m => m.Taunt);
            CloneTestHelpers.DoStatTest<bool>(m => m.Charge);
            CloneTestHelpers.DoStatTest<bool>(m => m.Windfury);
            CloneTestHelpers.DoStatTest<bool>(m => m.DivineShield);
            CloneTestHelpers.DoStatTest<bool>(m => m.Stealth);
        }

        [TestMethod()]
        public void CloneTest_AurasClonedProperly()
        {
            Battlefield bf = TestBattlefield.New();
            Field f = bf.Player.Field;
            f.Add(new ChillwindYeti());
            f.Add(new FlametongTotem());
            f.Add(new ChillwindYeti());

            Minion cloned = (Minion)f[1].Clone();
            f.Insert(1, cloned);

            List<int> expectedAtks = new List<int> { 6, 2, 2, 6 };
            Assert.AreEqual(expectedAtks.Count, f.Count);
            for (int i = 0; i < expectedAtks.Count; i++)
            {
                Assert.AreEqual(expectedAtks[i], f[i].Atk);
            }
        }

        [TestMethod()]
        public void CloneTest_BattlecriesClonedProperly()
        {
            Minion minion = new AbusiveSergeant();
            Minion cloned = (Minion)minion.Clone();

            Assert.IsNotNull(cloned.BattlecryEffect);
            Assert.AreNotSame(minion.BattlecryEffect, cloned.BattlecryEffect);
        }

        [TestMethod()]
        public void CloneTest_DeathrattlesClonedProperly()
        {
            Minion minion = new Abomintaion();
            Minion cloned = (Minion)minion.Clone();

            Assert.IsNotNull(cloned.DeathrattleEffect);
            Assert.AreNotSame(minion.DeathrattleEffect, cloned.DeathrattleEffect);
        }
    }
}