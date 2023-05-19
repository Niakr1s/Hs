using HsLib.Types.Stats;

namespace HsLibTests.Types.Stats
{
    [TestClass()]
    public class HpTests
    {
        [TestMethod()]
        public void GetDamageTest()
        {
            Hp hp = new(30);

            HpGotDamageEventArgs? eventArgs = null;
            hp.GotDamage += (s, e) => eventArgs = e;

            hp.GetDamage(5);
            Assert.AreEqual(25, hp);
            Assert.IsNotNull(eventArgs);
            Assert.AreEqual(5, eventArgs.Amount);
        }
    }
}