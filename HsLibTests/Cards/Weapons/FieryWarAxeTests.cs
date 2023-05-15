using HsLib.Cards.Weapons;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLibTests.Helpers;

namespace HsLibTests.Cards.Weapons
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

            Assert.AreEqual(weapon, bf.Player.Weapon.Card);
        }
    }
}