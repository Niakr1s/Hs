using HsLib.KnownCards.Minions;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Containers;
using HsLib.Types.Places;
using HsLib.Types.Stats;

namespace HsLibTests.Types.Cards
{
    [TestClass()]
    public class MinionTests
    {
        [TestMethod()]
        public void CloneTest_StatsClonedProperly()
        {
            DoStatTest<int>(m => m.Mp);
            DoStatTest<int>(m => m.Atk);
            DoStatTest<int>(m => m.Hp);
            DoStatTest<bool>(m => m.Taunt);
            DoStatTest<bool>(m => m.Charge);
            DoStatTest<bool>(m => m.Windfury);
            DoStatTest<bool>(m => m.DivineShield);
            DoStatTest<bool>(m => m.Stealth);
        }

        [TestMethod()]
        public void CloneTest_PlaceClonedProperly()
        {
            Minion minion = new ChillwindYeti();
            minion.PlaceInContainer = new(Pid.P1, Loc.Hand, 0, 0);
            Minion cloned = minion.Clone();
            Assert.IsNull(cloned.PlaceInContainer);
        }

        [TestMethod()]
        public void CloneTest_AurasClonedProperly()
        {
            Battlefield bf = TestBattlefield.New();
            Field f = bf.Player.Field;
            f.Add(new ChillwindYeti());
            f.Add(new FlametongTotem());
            f.Add(new ChillwindYeti());

            Minion cloned = f[1].Clone();
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
            Minion cloned = minion.Clone();

            Assert.IsNotNull(cloned.BattlecryEffect);
            Assert.AreNotSame(minion.BattlecryEffect, cloned.BattlecryEffect);
        }

        [TestMethod()]
        public void CloneTest_DeathrattlesClonedProperly()
        {
            Minion minion = new Abomintaion();
            Minion cloned = minion.Clone();

            Assert.IsNotNull(cloned.DeathrattleEffect);
            Assert.AreNotSame(minion.DeathrattleEffect, cloned.DeathrattleEffect);
        }

        private static void DoStatTest<T>(Func<Minion, Stat<int>> statChooser)
            where T : struct
        {
            Minion yeti = new ChillwindYeti();
            var yetiStat = statChooser(yeti);
            int startValue = yetiStat;

            var buff = yetiStat.AddBuff(2);
            var aura = yetiStat.AddAura(2);

            Minion clonedYeti = yeti.Clone();
            var clonedYetiStat = statChooser(clonedYeti);

            Assert.AreEqual(startValue + 2, clonedYetiStat);

            buff.Deactivate();
            aura.Deactivate();
            Assert.AreEqual(startValue + 2, clonedYetiStat);
        }

        private static void DoStatTest<T>(Func<Minion, Stat<bool>> statChooser)
            where T : struct
        {
            Minion yeti = new ChillwindYeti();
            var yetiStat = statChooser(yeti);
            bool startValue = yetiStat;

            var buff = yetiStat.AddBuff(true);
            var aura = yetiStat.AddAura(true);

            Minion clonedYeti = yeti.Clone();
            var clonedYetiStat = statChooser(clonedYeti);

            Assert.AreEqual(true, clonedYetiStat);

            buff.Deactivate();
            aura.Deactivate();
            Assert.AreEqual(true, clonedYetiStat);
        }
    }
}