using HsLib.KnownCards.Minions;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Containers;
using HsLib.Types.Places;

namespace HsLibTests.KnownCards.Minions
{
    [TestClass()]
    public class FlametongTotemTests
    {
        [TestMethod()]
        public void FlametongTotemTest()
        {
            Board board = TestBoard.New();

            Minion totem = new FlametongTotem();
            Minion y1 = new ChillwindYeti();
            Minion y2 = new ChillwindYeti();

            Field f = board[Pid.P1].Field;

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
            Board board = TestBoard.New();

            Minion totem = new FlametongTotem();
            Minion y1 = new ChillwindYeti();

            Hand h = board[Pid.P1].Hand;
            h.Add(y1);

            board[Pid.P1].Hand.Add(totem);
            Assert.AreEqual(4, y1.Atk);
            Assert.AreEqual(0, totem.Atk);
        }
    }
}