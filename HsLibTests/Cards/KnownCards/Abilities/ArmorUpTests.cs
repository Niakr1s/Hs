using HsLib.Battle;
using HsLib.Cards.KnownCards.Minions;
using HsLib.Common.Place;
using HsLibTests;

namespace HsLib.Cards.KnownCards.Abilities.Tests
{
    [TestClass()]
    public class ArmorUpTests
    {
        [TestMethod()]
        public void ArmorUpTest()
        {
            Battlefield bf = TestBattlefield.New(p1: CardId.GarroshHellscream);
            Ability armorUp = bf.Player.Ability.Card;
            Assert.IsInstanceOfType(armorUp, typeof(ArmorUp));
            Assert.AreEqual(false, armorUp.EffectMustHaveTarget);
            Assert.AreEqual(0, armorUp.UseEffectTargets(bf).Count());


            Assert.AreEqual(0, bf.Player.Hero.Card.Armor.Value);
            Assert.AreEqual(true, bf.UseAbility());
            Assert.AreEqual(2, bf.Player.Hero.Card.Armor.Value);

            Minion y1 = new ChillwindYeti();
            bf[Pid.P1].Field.Add(y1);
            Assert.AreEqual(0, armorUp.UseEffectTargets(bf).Count());
        }
    }
}