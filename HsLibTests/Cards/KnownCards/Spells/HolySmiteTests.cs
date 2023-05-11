using HsLib.Battle;
using HsLib.Cards.KnownCards.Minions;
using HsLib.Common.Place;

namespace HsLib.Cards.KnownCards.Spells.Tests
{
    [TestClass()]
    public class HolySmiteTests
    {
        [TestMethod()]
        public void HolySmiteTest()
        {
            Battlefield bf = new Battlefield(CardId.JainaProudmoore, CardId.JainaProudmoore);
            bf.BattleService.BSRules = null;

            Spell holySmite = new HolySmite();
            bf[Pid.P1].Hand.Add(holySmite);
            Assert.AreEqual(true, holySmite.CanUseEffect(bf));
            Assert.AreEqual(2, holySmite.UseEffectTargets(bf).Count()); // mine and his heros

            Minion yeti = new ChillwindYeti();
            bf[Pid.P2].Field.Add(yeti);
            Assert.AreEqual(true, holySmite.CanUseEffect(bf));
            Assert.AreEqual(3, holySmite.UseEffectTargets(bf).Count());

            holySmite.UseEffect(bf, yeti);
            Assert.AreEqual(3, yeti.Hp.Value);
        }
    }
}