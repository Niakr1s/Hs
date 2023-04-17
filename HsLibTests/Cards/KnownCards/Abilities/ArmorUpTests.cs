using HsLib.Battle;
using HsLib.Common.Place;

namespace HsLib.Cards.KnownCards.Abilities.Tests
{
    [TestClass()]
    public class ArmorUpTests
    {
        [TestMethod()]
        public void ArmorUpTest()
        {
            Battlefield bf = new Battlefield(CardId.GarroshHellscream, CardId.GarroshHellscream);
            bf.BattleService.Rules = null;

            foreach (Pid pid in Pids.All())
            {
                Assert.IsInstanceOfType(bf[pid].Ability.Card, typeof(ArmorUp));
                Assert.AreEqual(30, bf[pid].Hero.Card.Hp.Value);
                Assert.AreEqual(false, bf.BattleService.UseAbility(pid, bf[pid].Hero.Card));
                Assert.AreEqual(0, bf[pid].Hero.Card.Armor.Value);
                Assert.AreEqual(true, bf.BattleService.UseAbility(pid));
                Assert.AreEqual(2, bf[pid].Hero.Card.Armor.Value);
            }
        }
    }
}