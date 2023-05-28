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
            minion.Place = new(Pid.P1, Loc.Hand);
            Minion cloned = (Minion)minion.Clone();
            Assert.AreEqual(true, cloned.Place.IsNone());
        }

        [TestMethod()]
        public void CloneTest_StatsClonedProperly()
        {
            CloneTestHelpers.DoStatTest<int>(m => m.Mp);
        }
    }
}