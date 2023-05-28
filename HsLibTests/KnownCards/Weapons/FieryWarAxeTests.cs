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
            Battlefield bf = TestBattlefield.New();

            Weapon weapon = new FieryWarAxe();
            bf.Player.Hand.Add(weapon);
            bf.Player.Hand.PlayFromHand(0)();

            Assert.AreEqual(weapon, bf.Player.Weapon);
        }
    }
}