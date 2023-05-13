using HsLib.Cards.Minions;
using HsLib.Cards.Spells;
using HsLib.Systems;
using HsLib.Types.Cards;


namespace HsLibTests.Cards.Spells
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