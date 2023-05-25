using HsLib.KnownCards.Minions;
using HsLib.Types.Cards;
using HsLib.Types.Places;

namespace HsLibTests.Types.Cards
{
    [TestClass()]
    public class CardTests
    {
        [TestMethod()]
        public void CloneTest_PlaceClonedProperly()
        {
            Minion minion = new ChillwindYeti();
            minion.PlaceInContainer = new(Pid.P1, Loc.Hand, 0, 0);
            Minion cloned = (Minion)minion.Clone();
            Assert.IsNull(cloned.PlaceInContainer);
        }

        [TestMethod()]
        public void CloneTest_StatsClonedProperly()
        {
            CloneTestHelpers.DoStatTest<int>(m => m.Mp);
        }
    }
}