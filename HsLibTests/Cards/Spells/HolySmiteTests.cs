using HsLib.Cards.Minions;
using HsLib.Cards.Spells;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLibTests.Helpers;

namespace HsLibTests.Cards.Spells
{
    [TestClass()]
    public class HolySmiteTests
    {
        [TestMethod()]
        public void HolySmiteTest()
        {
            Battlefield bf = TestBattlefield.New();

            Spell holySmite = new HolySmite();
            bf.Player.Hand.Add(holySmite);
            Assert.AreEqual(2, holySmite.UseEffectTargets(bf, bf.Player.Pid).Count()); // mine and his heros

            Minion yeti = new ChillwindYeti();
            bf.Enemy.Field.Add(yeti);
            Assert.AreEqual(3, holySmite.UseEffectTargets(bf, bf.Player.Pid).Count());

            holySmite.UseEffect(bf, bf.Player.Pid, yeti);
            Assert.AreEqual(3, yeti.Hp.Value);
        }
    }
}