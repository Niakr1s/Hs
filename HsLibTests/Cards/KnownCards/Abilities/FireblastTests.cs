using HsLib.Battle;
using HsLib.Cards.KnownCards.Minions;
using HsLib.Common.Place;

namespace HsLib.Cards.KnownCards.Abilities.Tests
{
    [TestClass()]
    public class FireblastTests
    {

        [TestMethod()]
        public void UseEffectTest()
        {
            Battlefield bf = new Battlefield(CardId.JainaProudmoore, CardId.JainaProudmoore);
            bf.Turn.Start();
            Assert.IsInstanceOfType(bf[Pid.P1].Ability.Card, typeof(Fireblast));

            foreach (Pid pid in Pids.All())
            {
                Assert.AreEqual(30, bf[pid].Hero.Card.Hp.Value);
                bf.UseAbility(bf[pid].Hero.Card);
                Assert.AreEqual(29, bf[pid].Hero.Card.Hp.Value);
            }

            Minion y1 = new ChillwindYeti();
            Minion y2 = new ChillwindYeti();

            bf[Pid.P1].Field.Add(y1);
            bf[Pid.P2].Field.Add(y2);
            Assert.AreEqual(5, y1.Hp.Value);
            Assert.AreEqual(5, y2.Hp.Value);

            bf.UseAbility(y1);
            Assert.AreEqual(4, y1.Hp.Value);

            bf.UseAbility(y2);
            Assert.AreEqual(4, y2.Hp.Value);
        }
    }
}