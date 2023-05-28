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
            var board = TestBoard.New();

            Assert.AreEqual(1, chooser.ChooseCards(board, board.Player.Ability).Count());
            Assert.AreEqual(1, chooser.ChooseCards(board, board.Enemy.Ability).Count());
        }
    }
}