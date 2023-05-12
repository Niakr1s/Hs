using HsLib.Battle;
using HsLib.Cards.KnownCards.Minions;

namespace HsLib.Cards.KnownCards.Spells.Tests
{
    [TestClass()]
    public class HolySmiteTests
    {
        [TestMethod()]
        public void HolySmiteTest()
        {
            Battlefield bf = new Battlefield(CardId.JainaProudmoore, CardId.JainaProudmoore);
            bf.Turn.Start();

            Spell holySmite = new HolySmite();
            bf.Player.Hand.Add(holySmite);
            Assert.AreEqual(2, holySmite.UseEffectTargets(bf).Count()); // mine and his heros

            Minion yeti = new ChillwindYeti();
            bf.Enemy.Field.Add(yeti);
            Assert.AreEqual(3, holySmite.UseEffectTargets(bf).Count());

            holySmite.UseEffect(bf, yeti);
            Assert.AreEqual(3, yeti.Hp.Value);
        }
    }
}