using HsLib.KnownCards.Minions;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLibTests.Helpers;

namespace HsLibTests.KnownCards.Minions
{
    [TestClass()]
    public class DruidOfTheClawTests
    {
        [TestMethod()]
        public void DruidOfTheClawTest()
        {
            Battlefield bf = TestBattlefield.New();

            Minion m = new DruidOfTheClaw();
            bf.Player.Hand.Add(m);

            Assert.AreEqual(true, bf.PlayFromHand(0));
            Assert.IsNotInstanceOfType(bf.Player.Field[0], typeof(DruidOfTheClaw));

            Assert.AreEqual(0, bf.Player.Hand.Count);
        }
    }
}