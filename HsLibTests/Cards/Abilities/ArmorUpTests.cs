using HsLib.Cards.Abilities;
using HsLib.Cards.Minions;
using HsLib.Systems;
using HsLib.Types;
using HsLib.Types.Cards;
using HsLibTests.Helpers;

namespace HsLibTests.Cards.Abilities
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
            Assert.AreEqual(0, armorUp.AbilityEffect.UseEffectTargets(bf, bf.Player.Pid).Count());


            Assert.AreEqual(0, bf.Player.Hero.Card.Armor.Value);
            Assert.AreEqual(true, bf.UseAbility());
            Assert.AreEqual(2, bf.Player.Hero.Card.Armor.Value);

            Minion y1 = new ChillwindYeti();
            bf[Pid.P1].Field.Add(y1);
            Assert.AreEqual(0, armorUp.AbilityEffect.UseEffectTargets(bf, bf.Player.Pid).Count());
        }
    }
}