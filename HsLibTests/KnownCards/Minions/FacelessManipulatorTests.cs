using HsLib.KnownCards.Minions;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Containers;

namespace HsLibTests.KnownCards.Minions
{
    [TestClass()]
    public class FacelessManipulatorTests
    {
        [TestMethod()]
        public void FacelessManipulatorTest()
        {
            Board board = TestBoard.New();

            Minion yeti = new ChillwindYeti();
            Field f = board.Player.Field;
            f.Add(yeti);
            board.Player.Hand.Add(new FacelessManipulator());

            board.PlayFromHand(0, effectTarget: yeti);
            Assert.AreEqual(2, f.Count);
            Assert.IsInstanceOfType(f[1], typeof(ChillwindYeti));
            Assert.AreNotSame(f[0], f[1]);
            Assert.AreEqual(f[0].Mp.Value, f[1].Mp.Value);
            Assert.AreEqual(f[0].Atk.Value, f[1].Atk.Value);
            Assert.AreEqual(f[0].Hp.Value, f[1].Hp.Value);
        }
    }
}