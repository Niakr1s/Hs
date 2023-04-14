using Models.Cards;
using Models.Cards.KnownCards.Minions;
using Models.Common.Place;
using Models.Containers;

namespace Models.Services.Battle.Tests
{
    [TestClass()]
    public class BattleServiceRulesTests
    {
        [TestMethod()]
        public void CanMeleeAttackTest()
        {
            Battlefield bf = new(HeroId.Jaina, HeroId.Rexxar);
            BattleServiceRules rules = new BattleServiceRules(bf);

            Minion p1Yeti = new ChillwindYeti();
            Minion p2Yeti = new ChillwindYeti();
            Hero p1Hero = bf[Pid.P1].Hero;
            Hero p2Hero = bf[Pid.P2].Hero;

            bf[Pid.P1].Hand.Add(p1Yeti);
            bf[Pid.P2].Hand.Add(p2Yeti);
            Assert.AreEqual(false, rules.CanMeleeAttack(p1Yeti, p2Yeti));
            Assert.AreEqual(false, rules.CanMeleeAttack(p2Yeti, p1Yeti));

            bf[Pid.P2].Hand.Remove(p2Yeti);
            bf[Pid.P2].Field.Add(p2Yeti);
            Assert.AreEqual(false, rules.CanMeleeAttack(p1Yeti, p2Yeti));
            Assert.AreEqual(false, rules.CanMeleeAttack(p2Yeti, p1Yeti));
            Assert.AreEqual(true, rules.CanMeleeAttack(p2Yeti, p1Hero));
            Assert.AreEqual(false, rules.CanMeleeAttack(p2Yeti, p2Hero));

            bf[Pid.P1].Hand.Remove(p1Yeti);
            bf[Pid.P1].Field.Add(p1Yeti);
            Assert.AreEqual(true, rules.CanMeleeAttack(p1Yeti, p2Yeti));
            Assert.AreEqual(true, rules.CanMeleeAttack(p2Yeti, p1Yeti));
            Assert.AreEqual(true, rules.CanMeleeAttack(p1Yeti, p2Hero));
            Assert.AreEqual(false, rules.CanMeleeAttack(p1Yeti, p1Hero));
            Assert.AreEqual(true, rules.CanMeleeAttack(p2Yeti, p1Hero));
            Assert.AreEqual(false, rules.CanMeleeAttack(p2Yeti, p2Hero));

            Minion p2YetiGuardian = new ChillwindYeti();
            bf[Pid.P2].Field.Add(p2YetiGuardian);
            Assert.AreEqual(true, rules.CanMeleeAttack(p1Yeti, p2Yeti));

            p2YetiGuardian.Taunt.Set(true);
            Assert.AreEqual(false, rules.CanMeleeAttack(p1Yeti, p2Yeti));

            p2YetiGuardian.Stealth.Set(true);
            Assert.AreEqual(true, rules.CanMeleeAttack(p1Yeti, p2Yeti));
            p2YetiGuardian.Stealth.Set(false);
            Assert.AreEqual(false, rules.CanMeleeAttack(p1Yeti, p2Yeti));

            p2Yeti.Taunt.Set(true);
            Assert.AreEqual(true, rules.CanMeleeAttack(p1Yeti, p2Yeti));

            p2Yeti.Stealth.Set(true);
            Assert.AreEqual(false, rules.CanMeleeAttack(p1Yeti, p2Yeti));
        }
    }
}