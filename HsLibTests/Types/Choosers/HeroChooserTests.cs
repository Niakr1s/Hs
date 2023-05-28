using HsLib.Types.Choosers;

namespace HsLibTests.Types.Choosers
{
    [TestClass()]
    public class HeroChooserTests
    {
        [TestMethod()]
        public void HeroChooserTest()
        {
            var chooser = new HeroChooser();
            var bf = TestBattlefield.New();

            Assert.AreEqual(1, chooser.ChooseCards(bf, bf.Player.Ability).Count());
            Assert.AreEqual(1, chooser.ChooseCards(bf, bf.Enemy.Ability).Count());
        }
    }
}