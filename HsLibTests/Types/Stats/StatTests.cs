using HsLib.Types.Stats;

namespace HsLibTests.Types.Stats
{
    [TestClass()]
    public class StatTests
    {
        [TestMethod()]
        public void StatValueTest()
        {
            IntStat stat = new(1);
            Assert.AreEqual(1, stat.Value);
        }

        [TestMethod()]
        public void StatChangedTest_Set()
        {
            IntStat stat = new(1);

            var buff = stat.AddBuff(1);
            var aura = stat.AddAura(1);

            List<StatChangedEventArgs> events = new();
            stat.StatChanged += (s, e) => events.Add(e);

            stat.Set(10);
            Assert.AreEqual(2, events.Count);
            Assert.AreEqual(StatChangedEventType.BuffRemoved, events[0].Type);
            Assert.AreEqual(StatChangedEventType.ValueSet, events[1].Type);
        }

        [TestMethod()]
        public void StatChangedTest_Reset()
        {
            IntStat stat = new(1);

            var buff = stat.AddBuff(1);
            var aura = stat.AddAura(1);

            List<StatChangedEventArgs> events = new();
            stat.StatChanged += (s, e) => events.Add(e);

            stat.Reset();
            Assert.AreEqual(3, events.Count);
            Assert.AreEqual(StatChangedEventType.BuffRemoved, events[0].Type);
            Assert.AreEqual(StatChangedEventType.AuraRemoved, events[1].Type);
            Assert.AreEqual(StatChangedEventType.Reset, events[2].Type);
        }

        [TestMethod()]
        public void StatChangedTest_Buff()
        {
            List<StatChangedEventArgs> events = new();
            IntStat stat = new(1);
            stat.StatChanged += (s, e) => events.Add(e);

            var buff = stat.AddBuff(1);
            Assert.AreEqual(StatChangedEventType.BuffAdded, events[^1].Type);
            Assert.AreEqual(1, events.Count);

            buff.Deactivate();
            Assert.AreEqual(StatChangedEventType.BuffRemoved, events[^1].Type);
            Assert.AreEqual(2, events.Count);
        }

        [TestMethod()]
        public void StatChangedTest_Aura()
        {
            List<StatChangedEventArgs> events = new();
            IntStat stat = new(1);
            stat.StatChanged += (s, e) => events.Add(e);

            var aura = stat.AddAura(1);
            Assert.AreEqual(StatChangedEventType.AuraAdded, events[^1].Type);
            Assert.AreEqual(1, events.Count);

            aura.Deactivate();
            Assert.AreEqual(StatChangedEventType.AuraRemoved, events[^1].Type);
            Assert.AreEqual(2, events.Count);
        }

        [TestMethod()]
        public void AddBuffTest()
        {
            IntStat stat = new(1);
            Assert.AreEqual(1, stat.Value);

            Enchant<int> buff = stat.AddBuff(2);
            Assert.AreEqual(3, stat.Value);

            buff.Deactivate();
            Assert.AreEqual(1, stat.Value);
        }

        [TestMethod()]
        public void AddAuraTest()
        {
            IntStat stat = new(1);
            Assert.AreEqual(1, stat.Value);

            Enchant<int> buff = stat.AddAura(2);
            Assert.AreEqual(3, stat.Value);

            buff.Deactivate();
            Assert.AreEqual(1, stat.Value);
        }

        [TestMethod()]
        public void SetTest_WithNoEnchants()
        {
            IntStat stat = new(1);
            Assert.AreEqual(1, stat.Value);

            stat.Set(5);
            Assert.AreEqual(5, stat.Value);
        }

        [TestMethod()]
        public void SetTest_WithEnchants()
        {
            const int initialValue = 1;
            IntStat stat = new(initialValue);
            Assert.AreEqual(initialValue, stat.Value);

            List<Enchant<int>> buffs = new();
            for (int i = 2; i < 10; i++)
            {
                buffs.Add(stat.AddBuff(i));
            }

            List<Enchant<int>> auras = new();
            for (int i = 1; i < 5; i++)
            {
                auras.Add(stat.AddAura(i));
            }

            const int setValue = 5;
            stat.Set(setValue);

            buffs.ForEach(b => Assert.AreEqual(false, b.Active));
            auras.ForEach(a => Assert.AreEqual(true, a.Active));

            Assert.AreEqual(setValue + auras.Select(a => a.Value).Aggregate((a, b) => a + b), stat.Value);
        }

        [TestMethod()]
        public void ResetTest()
        {
            const int initialValue = 1;
            IntStat stat = new(initialValue);
            Assert.AreEqual(initialValue, stat.Value);

            List<Enchant<int>> buffs = new();
            for (int i = 2; i < 10; i++)
            {
                buffs.Add(stat.AddBuff(i));
            }

            List<Enchant<int>> auras = new();
            for (int i = 1; i < 5; i++)
            {
                auras.Add(stat.AddAura(i));
            }

            stat.Reset();

            buffs.ForEach(b => Assert.AreEqual(false, b.Active));
            auras.ForEach(a => Assert.AreEqual(false, a.Active));

            Assert.AreEqual(initialValue, stat.Value);
        }
    }
}