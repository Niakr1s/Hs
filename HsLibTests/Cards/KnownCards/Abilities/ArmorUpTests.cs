using HsLib.Battle;

namespace HsLib.Cards.KnownCards.Abilities.Tests
{
    [TestClass()]
    public class ArmorUpTests
    {
        [TestMethod()]
        public void ArmorUpTest()
        {
            Battlefield bf = new Battlefield(CardId.GarroshHellscream, CardId.GarroshHellscream);
            bf.Turn.Start();

            Assert.AreEqual(0, bf.Player.Hero.Card.Armor.Value);
            Assert.AreEqual(true, bf.UseAbility());
            Assert.AreEqual(2, bf.Player.Hero.Card.Armor.Value);
        }
    }
}