﻿using HsLib.Battle;
using HsLib.Cards;
using HsLib.Cards.KnownCards.Minions;
using HsLib.Cards.KnownCards.Spells;
using HsLib.Common.Place;

namespace HsLibTests.Battle.Services
{
    [TestClass()]
    public class BattleServiceTests
    {
        [TestMethod()]
        public void CanTurnOffRulesTest()
        {
            Battlefield bf = new Battlefield(CardId.JainaProudmoore, CardId.JainaProudmoore);
            Minion yeti1 = new ChillwindYeti();
            Minion yeti2 = new ChillwindYeti();

            bf[Pid.P1].Deck.Add(yeti1);
            bf[Pid.P2].Deck.Add(yeti2);
            Assert.AreEqual(false, bf.BattleService.MeleeAttack(yeti1, yeti2));

            bf.BattleService.WithRules = false;
            Assert.AreEqual(true, bf.BattleService.MeleeAttack(yeti1, yeti2));
        }

        [TestMethod()]
        public void MinionAttackMinionTest()
        {
            Battlefield bf = new Battlefield(CardId.JainaProudmoore, CardId.JainaProudmoore);
            bf.BattleService.WithRules = false;
            Minion yeti1 = new ChillwindYeti();
            Minion yeti2 = new ChillwindYeti();

            bf[Pid.P1].Field.Add(yeti1);
            bf[Pid.P2].Field.Add(yeti2);

            Assert.AreEqual(5, yeti1.Hp.Value);
            Assert.AreEqual(5, yeti2.Hp.Value);

            Assert.AreEqual(true, bf.BattleService.MeleeAttack(yeti1, yeti2));
            Assert.AreEqual(1, yeti1.Hp.Value);
            Assert.AreEqual(1, yeti2.Hp.Value);

            Assert.AreEqual(true, bf.BattleService.MeleeAttack(yeti1, yeti2));
            Assert.AreEqual(0, yeti1.Hp.Value);
            Assert.AreEqual(0, yeti2.Hp.Value);
        }

        [TestMethod()]
        public void MinionAttackHeroTest()
        {
            Battlefield bf = new Battlefield(CardId.JainaProudmoore, CardId.JainaProudmoore);
            bf.BattleService.WithRules = false;

            Minion yeti1 = new ChillwindYeti();
            Hero p2Hero = bf[Pid.P2].Hero.Card;

            bf[Pid.P1].Field.Add(yeti1);
            Assert.AreEqual(5, yeti1.Hp.Value);
            Assert.AreEqual(30, p2Hero.Hp.Value);

            Assert.AreEqual(true, bf.BattleService.MeleeAttack(yeti1, p2Hero));
            Assert.AreEqual(5, yeti1.Hp.Value);
            Assert.AreEqual(26, p2Hero.Hp.Value);

            Assert.AreEqual(true, bf.BattleService.MeleeAttack(yeti1, p2Hero));
            Assert.AreEqual(5, yeti1.Hp.Value);
            Assert.AreEqual(22, p2Hero.Hp.Value);
        }

        [TestMethod()]
        public void WeaponAttackTest()
        {
            Battlefield bf = new Battlefield(CardId.JainaProudmoore, CardId.JainaProudmoore);
            bf.BattleService.WithRules = false;

            Minion yeti1 = new ChillwindYeti();
            bf[Pid.P2].Field.Add(yeti1);

            Weapon weapon = bf[Pid.P1].Weapon.Card;
            Assert.AreEqual(false, bf.BattleService.MeleeAttack(bf[Pid.P1].Weapon.Card, yeti1));

            weapon.Atk.AddBuff(2);
            int hpBefore = weapon.Hp.Value;
            Assert.AreEqual(true, bf.BattleService.MeleeAttack(bf[Pid.P1].Weapon.Card, yeti1));
            Assert.AreEqual(1, hpBefore - weapon.Hp.Value);
        }

        [TestMethod()]
        public void CastSpellTest()
        {
            Battlefield bf = new Battlefield(CardId.JainaProudmoore, CardId.JainaProudmoore);

            Spell mindControl = new MindControl();
            Minion yeti = new ChillwindYeti();
            bf[Pid.P2].Field.Add(yeti);
            Assert.AreEqual(false, bf.BattleService.CastSpell(mindControl, yeti));

            bf[Pid.P1].Deck.Add(mindControl);
            Assert.AreEqual(false, bf.BattleService.CastSpell(mindControl, yeti));

            bf[Pid.P1].Deck.Remove(mindControl);
            bf[Pid.P1].Hand.Add(mindControl);
            Assert.AreEqual(false, bf.BattleService.CastSpell(mindControl, yeti));

            bf[Pid.P1].Mp.Set(10);
            Assert.AreEqual(false, bf.BattleService.CastSpell(mindControl, yeti));

            bf.Turn.Next();
            Assert.AreEqual(true, bf.BattleService.CastSpell(mindControl, yeti));
            Assert.AreEqual(0, bf[Pid.P1].Hand.Cards.Count());
            Assert.AreEqual(1, bf[Pid.P1].Graveyard.Cards.Count());
        }
    }
}
