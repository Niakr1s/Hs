using HsLib.Cards.Minions;
using HsLib.Systems;
using HsLib.Types;
using HsLib.Types.Cards;
using HsLib.Types.Containers;
using HsLibTests.Helpers;

namespace HsLibTests.Cards.Minions
{
    [TestClass()]
    public class FlametongTotemTests
    {
        [TestMethod()]
        public void FlametongTotemTest()
        {
            Battlefield bf = TestBattlefield.New();

            Minion totem = new FlametongTotem();
            Minion y1 = new ChillwindYeti();
            Minion y2 = new ChillwindYeti();

            Field f = bf[Pid.P1].Field;

            f.Add(y1);
            Assert.AreEqual(0, totem.Atk);
            Assert.AreEqual(4, y1.Atk);
            Assert.AreEqual(4, y2.Atk);

            f.Add(totem);
            Assert.AreEqual(6, y1.Atk);
            Assert.AreEqual(0, totem.Atk);

            f.Add(y2);
            Assert.AreEqual(6, y1.Atk);
            Assert.AreEqual(0, totem.Atk);
            Assert.AreEqual(6, y2.Atk);

            Minion y12 = new ChillwindYeti();
            f.Insert(1, y12); // placing after y1, and before of totem
            Assert.AreEqual(4, y1.Atk);
            Assert.AreEqual(6, y12.Atk);
            Assert.AreEqual(0, totem.Atk);
            Assert.AreEqual(6, y2.Atk);

            f.Remove(y12);
            Assert.AreEqual(6, y1.Atk);
            Assert.AreEqual(0, totem.Atk);
            Assert.AreEqual(6, y2.Atk);

            f.Remove(totem);
            Assert.AreEqual(4, y1.Atk);
            Assert.AreEqual(4, y2.Atk);
            Assert.AreEqual(4, y12.Atk);
        }

        [TestMethod()]
        public void FlametongWorksOnlyAtFieldTest()
        {
            Battlefield bf = TestBattlefield.New();

            Minion totem = new FlametongTotem();
            Minion y1 = new ChillwindYeti();

            Hand h = bf[Pid.P1].Hand;
            h.Add(y1);

            bf[Pid.P1].Hand.Add(totem);
            Assert.AreEqual(4, y1.Atk);
            Assert.AreEqual(0, totem.Atk);
        }
    }
}