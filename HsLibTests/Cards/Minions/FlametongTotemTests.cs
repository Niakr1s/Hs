using HsLib.Cards.Minions;
using HsLib.Systems;
using HsLib.Types;
using HsLib.Types.Cards;
using HsLib.Types.Containers;

namespace HsLibTests.Cards.Minions
{
    [TestClass()]
    public class FlametongTotemTests
    {
        [TestMethod()]
        public void FlametongTotemTest()
        {
            Battlefield bf = new Battlefield(CardId.JainaProudmoore, CardId.JainaProudmoore);

            Minion totem = new FlametongTotem();
            Minion y1 = new ChillwindYeti();
            Minion y2 = new ChillwindYeti();

            Field f = bf[Pid.P1].Field;

            f.Add(y1);
            Assert.AreEqual(0, totem.Atk.Value);
            Assert.AreEqual(4, y1.Atk.Value);
            Assert.AreEqual(4, y2.Atk.Value);

            f.Add(totem);
            Assert.AreEqual(6, y1.Atk.Value);
            Assert.AreEqual(0, totem.Atk.Value);

            f.Add(y2);
            Assert.AreEqual(6, y1.Atk.Value);
            Assert.AreEqual(0, totem.Atk.Value);
            Assert.AreEqual(6, y2.Atk.Value);

            Minion y12 = new ChillwindYeti();
            f.Insert(1, y12); // placing after y1, and before of totem
            Assert.AreEqual(4, y1.Atk.Value);
            Assert.AreEqual(6, y12.Atk.Value);
            Assert.AreEqual(0, totem.Atk.Value);
            Assert.AreEqual(6, y2.Atk.Value);

            f.Remove(y12);
            Assert.AreEqual(6, y1.Atk.Value);
            Assert.AreEqual(0, totem.Atk.Value);
            Assert.AreEqual(6, y2.Atk.Value);

            f.Remove(totem);
            Assert.AreEqual(4, y1.Atk.Value);
            Assert.AreEqual(4, y2.Atk.Value);
            Assert.AreEqual(4, y12.Atk.Value);
        }

        [TestMethod()]
        public void FlametongWorksOnlyAtFieldTest()
        {
            Battlefield bf = new Battlefield(CardId.JainaProudmoore, CardId.JainaProudmoore);

            Minion totem = new FlametongTotem();
            Minion y1 = new ChillwindYeti();

            Hand h = bf[Pid.P1].Hand;
            h.Add(y1);

            bf[Pid.P1].Hand.Add(totem);
            Assert.AreEqual(4, y1.Atk.Value);
            Assert.AreEqual(0, totem.Atk.Value);
        }
    }
}