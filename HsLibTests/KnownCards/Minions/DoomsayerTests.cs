using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Containers;
using HsLib.Types.Places;

namespace HsLib.KnownCards.Minions
{
    [TestClass()]
    public class DoomsayerTests
    {
        [TestMethod()]
        public void DoomsayerTest()
        {
            Battlefield bf = TestBattlefield.New();

            Minion doomsayer = new Doomsayer();
            Minion y1 = new ChillwindYeti();
            Minion y2 = new ChillwindYeti();
            Minion enemyYeti = new ChillwindYeti();

            Field playerField = bf[Pid.P1].Field;
            Field enemyField = bf[Pid.P2].Field;

            enemyField.Add(enemyYeti);
            playerField.Add(y1);
            playerField.Add(doomsayer);
            playerField.Add(y2);

            bf.Turn.Skip(Pid.P1);
            Assert.AreEqual(0, playerField.Count);
            Assert.AreEqual(0, enemyField.Count);

            // make sure, it wont occurs again
            playerField.Add(new ChillwindYeti());
            enemyField.Add(new ChillwindYeti());
            bf.Turn.Skip(Pid.P1);
            Assert.AreNotEqual(0, playerField.Count);
            Assert.AreNotEqual(0, enemyField.Count);
        }
    }
}