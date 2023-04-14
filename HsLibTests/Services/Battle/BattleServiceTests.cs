﻿using Models.Cards;
using Models.Cards.KnownCards.Minions;
using Models.Common.Place;
using Models.Containers;

namespace Models.Services.Battle.Tests
{
    [TestClass()]
    public class BattleServiceTests
    {
        [TestMethod()]
        public void RulesTest()
        {
            Battlefield bf = new Battlefield(HeroId.Jaina, HeroId.Rexxar);
            Minion yeti1 = new ChillwindYeti();
            Minion yeti2 = new ChillwindYeti();

            bf[Pid.P1].Deck.Add(yeti1);
            bf[Pid.P2].Deck.Add(yeti2);
            Assert.AreEqual(false, bf.BattleService.MinionAttack(yeti1, yeti2));

            bf.BattleService.Rules = null;
            Assert.AreEqual(true, bf.BattleService.MinionAttack(yeti1, yeti2));
        }

        [TestMethod()]
        public void MinionAttackMinionTest()
        {
            Battlefield bf = new Battlefield(HeroId.Jaina, HeroId.Rexxar);
            Minion yeti1 = new ChillwindYeti();
            Minion yeti2 = new ChillwindYeti();

            bf[Pid.P1].Field.Add(yeti1);
            bf[Pid.P2].Field.Add(yeti2);

            Assert.AreEqual(5, yeti1.Hp.Value);
            Assert.AreEqual(5, yeti2.Hp.Value);

            Assert.AreEqual(true, bf.BattleService.MinionAttack(yeti1, yeti2));
            Assert.AreEqual(1, yeti1.Hp.Value);
            Assert.AreEqual(1, yeti2.Hp.Value);

            Assert.AreEqual(true, bf.BattleService.MinionAttack(yeti1, yeti2));
            Assert.AreEqual(1, yeti1.Hp.Value);
            Assert.AreEqual(0, yeti2.Hp.Value);
        }

        [TestMethod()]
        public void MinionAttackHeroTest()
        {
            Battlefield bf = new Battlefield(HeroId.Jaina, HeroId.Rexxar);
            Minion yeti1 = new ChillwindYeti();
            Hero p2Hero = bf[Pid.P2].Hero;

            bf[Pid.P1].Field.Add(yeti1);
            Assert.AreEqual(5, yeti1.Hp.Value);
            Assert.AreEqual(30, p2Hero.Hp.Value);

            Assert.AreEqual(true, bf.BattleService.MinionAttack(yeti1, p2Hero));
            Assert.AreEqual(5, yeti1.Hp.Value);
            Assert.AreEqual(26, p2Hero.Hp.Value);

            Assert.AreEqual(true, bf.BattleService.MinionAttack(yeti1, p2Hero));
            Assert.AreEqual(5, yeti1.Hp.Value);
            Assert.AreEqual(22, p2Hero.Hp.Value);
        }
    }
}