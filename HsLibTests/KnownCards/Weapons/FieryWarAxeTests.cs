using HsLib.KnownCards.Weapons;
using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLibTests.KnownCards.Weapons
{
    [TestClass()]
    public class FieryWarAxeTests
    {
        [TestMethod()]
        public void FieryWarAxeTest()
        {
            Board board = TestBoard.New();

            for (int i = 0; i < 3; i++)
            {
                Weapon weapon = new FieryWarAxe();
                board.Player.Hand.Add(weapon);
                board.Player.Hand.PlayFromHand(0)();

                Assert.AreEqual(weapon, board.Player.Weapon);
            }
        }
    }
}