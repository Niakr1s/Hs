using HsLib.KnownCards.Minions;
using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLibTests.KnownCards.Minions
{
    [TestClass()]
    public class DruidOfTheClawTests
    {
        [TestMethod()]
        public void DruidOfTheClawTest()
        {
            Board board = TestBoard.New();

            Minion m = new DruidOfTheClaw();
            board.Player.Hand.Add(m);

            Assert.AreEqual(true, board.PlayFromHand(0));
            Assert.IsNotInstanceOfType(board.Player.Field[0], typeof(DruidOfTheClaw));

            Assert.AreEqual(0, board.Player.Hand.Count);
        }
    }
}