using HsLib.KnownCards.Minions;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Containers;
using HsLib.Types.Places;

namespace HsLibTests.KnownCards.Minions
{
    [TestClass()]
    public class DoomsayerTests
    {
        [TestMethod()]
        public void DoomsayerTest()
        {
            Board board = TestBoard.New();

            Minion doomsayer = new Doomsayer();
            Minion y1 = new ChillwindYeti();
            Minion y2 = new ChillwindYeti();
            Minion enemyYeti = new ChillwindYeti();

            Field playerField = board[Pid.P1].Field;
            Field enemyField = board[Pid.P2].Field;

            enemyField.Add(enemyYeti);
            playerField.Add(y1);
            playerField.Add(doomsayer);
            playerField.Add(y2);

            board.Turn.Skip(Pid.P1);
            Assert.AreEqual(0, playerField.Count);
            Assert.AreEqual(0, enemyField.Count);

            // make sure, it wont occurs again
            playerField.Add(new ChillwindYeti());
            enemyField.Add(new ChillwindYeti());
            board.Turn.Skip(Pid.P1);
            Assert.AreNotEqual(0, playerField.Count);
            Assert.AreNotEqual(0, enemyField.Count);
        }
    }
}