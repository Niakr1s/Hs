using HsLib.KnownCards.Minions;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Places;

namespace HsLibTests.KnownCards.Minions
{
    [TestClass()]
    public class AbomintaionTests
    {
        [TestMethod()]
        public void AbomintaionTest()
        {
            Board board = TestBoard.New();
            Minion abom1 = new Abomintaion();
            Minion abom2 = new Abomintaion();
            Minion yeti1 = new ChillwindYeti();
            Minion yeti2 = new ChillwindYeti();

            board[Pid.P1].Field.Add(abom1);
            board[Pid.P2].Field.Add(abom2);
            board[Pid.P1].Field.Add(yeti1);
            board[Pid.P2].Field.Add(yeti2);

            board.Turn.Skip(board.Player.Pid);
            Assert.AreEqual(true, board.MinionAttack(0, Loc.Field, 0));
            Assert.AreEqual(0, abom1.Hp);
            Assert.AreEqual(0, abom1.Hp);
            Assert.AreEqual(1, yeti1.Hp);
            Assert.AreEqual(1, yeti2.Hp);
        }
    }
}