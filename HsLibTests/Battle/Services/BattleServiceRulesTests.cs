using HsLib.Battle;
using HsLib.Battle.Services;
using HsLib.Cards;
using HsLib.Cards.KnownCards.Abilities;
using HsLib.Cards.KnownCards.Minions;
using HsLib.Cards.KnownCards.Spells;
using HsLib.Common.Interfaces;
using HsLib.Common.Place;

namespace HsLibTests.Battle.Services
{
    [TestClass()]
    public class BattleServiceRulesTests
    {
        [TestMethod()]
        public void CanBeMeleeAttackedTest()
        {
            Battlefield bf = new(CardId.JainaProudmoore, CardId.JainaProudmoore);
            BattleServiceRules rules = new BattleServiceRules(bf);

            Minion y1 = new ChillwindYeti();
            Minion y2 = new ChillwindYeti();
            Hero p1 = bf[Pid.P1].Hero.Card;
            Hero p2 = bf[Pid.P2].Hero.Card;
            TestAttackSelf();
            Assert.AreEqual(true, rules.CanBeMeleeAttacked(p1, p2));
            Assert.AreEqual(true, rules.CanBeMeleeAttacked(p2, p1));
            Assert.AreEqual(false, rules.CanBeMeleeAttacked(p1, y2));
            Assert.AreEqual(false, rules.CanBeMeleeAttacked(p2, y1));

            bf[Pid.P1].Hand.Add(y1);
            bf[Pid.P2].Hand.Add(y2);
            TestAttackSelf();
            Assert.AreEqual(true, rules.CanBeMeleeAttacked(p1, p2));
            Assert.AreEqual(true, rules.CanBeMeleeAttacked(p2, p1));
            Assert.AreEqual(false, rules.CanBeMeleeAttacked(p1, y2));
            Assert.AreEqual(false, rules.CanBeMeleeAttacked(p2, y1));

            bf[Pid.P1].Hand.Remove(y1);
            bf[Pid.P1].Field.Add(y1);
            TestAttackSelf();
            Assert.AreEqual(false, rules.CanBeMeleeAttacked(p1, y2));
            Assert.AreEqual(true, rules.CanBeMeleeAttacked(p2, y1));
            Assert.AreEqual(true, rules.CanBeMeleeAttacked(p1, p2));
            Assert.AreEqual(true, rules.CanBeMeleeAttacked(p2, p1));
            // we won't check p2's attacks anymore

            bf[Pid.P2].Hand.Remove(y2);
            bf[Pid.P2].Field.Add(y2);
            TestAttackSelf();
            Assert.AreEqual(true, rules.CanBeMeleeAttacked(p1, y2));
            Assert.AreEqual(true, rules.CanBeMeleeAttacked(p1, p2));

            Minion y2Guardian = new ChillwindYeti();
            bf[Pid.P2].Field.Add(y2Guardian);
            TestAttackSelf();
            Assert.AreEqual(true, rules.CanBeMeleeAttacked(p1, y2));
            Assert.AreEqual(true, rules.CanBeMeleeAttacked(p1, p2));
            Assert.AreEqual(true, rules.CanBeMeleeAttacked(p1, y2Guardian));

            y2Guardian.Taunt.Set(true);
            TestAttackSelf();
            Assert.AreEqual(false, rules.CanBeMeleeAttacked(p1, y2));
            Assert.AreEqual(false, rules.CanBeMeleeAttacked(p1, p2));
            Assert.AreEqual(true, rules.CanBeMeleeAttacked(p1, y2Guardian));

            y2Guardian.Stealth.Set(true);
            TestAttackSelf();
            Assert.AreEqual(true, rules.CanBeMeleeAttacked(p1, y2));
            Assert.AreEqual(true, rules.CanBeMeleeAttacked(p1, p2));
            Assert.AreEqual(false, rules.CanBeMeleeAttacked(p1, y2Guardian));
            y2Guardian.Stealth.Set(false);

            y2.Taunt.Set(true);
            TestAttackSelf();
            Assert.AreEqual(true, rules.CanBeMeleeAttacked(p1, y2));
            Assert.AreEqual(false, rules.CanBeMeleeAttacked(p1, p2));
            Assert.AreEqual(true, rules.CanBeMeleeAttacked(p1, y2Guardian));

            y2.Stealth.Set(true);
            TestAttackSelf();
            Assert.AreEqual(false, rules.CanBeMeleeAttacked(p1, y2));
            Assert.AreEqual(false, rules.CanBeMeleeAttacked(p1, p2));
            Assert.AreEqual(true, rules.CanBeMeleeAttacked(p1, y2Guardian));

            void TestAttackSelf()
            {
                Assert.AreEqual(false, rules.CanBeMeleeAttacked(p1, p1));
                Assert.AreEqual(false, rules.CanBeMeleeAttacked(p2, p2));
                Assert.AreEqual(false, rules.CanBeMeleeAttacked(p1, y1));
                Assert.AreEqual(false, rules.CanBeMeleeAttacked(p2, y2));
            }
        }

        [TestMethod()]
        public void CanMeleeAttackWeaponTest()
        {
            Battlefield bf = new(CardId.JainaProudmoore, CardId.JainaProudmoore);
            Weapon w1 = bf[Pid.P1].Weapon.Card;
            CanMeleeAttackTest(bf, w1);
        }

        [TestMethod()]
        public void CanMeleeAttackMinionTest()
        {
            Battlefield bf = new(CardId.JainaProudmoore, CardId.JainaProudmoore);
            Minion y1 = new ChillwindYeti();
            bf[Pid.P1].Field.Add(y1);
            CanMeleeAttackTest(bf, y1);
        }

        private static void CanMeleeAttackTest(Battlefield bf, IAttacker attacker)
        {
            BattleServiceRules rules = new BattleServiceRules(bf);

            // shoudn't attack on 0 turn
            if (attacker.Atk.Value <= 0)
            {
                attacker.Atk.AddBuff(1);
                DoTest(false);
            }
            attacker.Charge.AddBuff(true);
            DoTest(false);
            attacker.Charge.Set(false);
            DoTest(false);

            // waiting enemy turn
            bf.Turn.Skip(attacker.Pid.He());
            DoTest(false);

            // waiting self turn and simulating first turn
            bf.Turn.Skip(attacker.Pid);
            attacker.TurnAdded = bf.Turn.No;
            DoTest(attacker.Charge.Value);
            attacker.Charge.AddBuff(true);
            DoTest(true);
            attacker.Charge.Reset();
            DoTest(attacker.Charge.Value);

            // new turn
            bf.Turn.Skip(attacker.Pid);
            DoTest(true);
            // doing 1st attack
            Assert.AreEqual(true, bf.BattleService.MeleeAttack(attacker, bf[Pid.P2].Hero.Card));
            DoTest(false);
            // adding windfury
            attacker.Windfury.AddBuff(true);
            DoTest(true);
            // doing 2 attack
            Assert.AreEqual(true, bf.BattleService.MeleeAttack(attacker, bf[Pid.P2].Hero.Card));
            DoTest(false);

            // checking if he can attack only self turn
            bf.Turn.Next();
            DoTest(false);
            bf.Turn.Next();
            DoTest(true);

            void DoTest(bool expected)
            {
                Assert.AreEqual(expected, rules.CanMeleeAttack(attacker, bf.Turn));
            }
        }

        [TestMethod()]
        public void CanUseEffectTest()
        {
            Battlefield bf = new(CardId.GarroshHellscream, CardId.JainaProudmoore);
            BattleServiceRules rules = new BattleServiceRules(bf);

            Spell targetEffect = new MindControl();
            Ability nonTargetEffect = new ArmorUp();

            Minion y = new ChillwindYeti();

            bf[Pid.P1].Deck.Add(targetEffect);
            bf[Pid.P1].Deck.Add(nonTargetEffect);
            Assert.AreEqual(false, rules.CanUseEffect(targetEffect, null));
            Assert.AreEqual(false, rules.CanUseEffect(targetEffect, y));
            Assert.AreEqual(true, rules.CanUseEffect(nonTargetEffect, null));
            Assert.AreEqual(false, rules.CanUseEffect(nonTargetEffect, y));

            bf[Pid.P1].Deck.Remove(targetEffect);
            bf[Pid.P1].Deck.Remove(nonTargetEffect);
            bf[Pid.P1].Hand.Add(targetEffect);
            bf[Pid.P1].Hand.Add(nonTargetEffect);
            Assert.AreEqual(false, rules.CanUseEffect(targetEffect, null));
            Assert.AreEqual(false, rules.CanUseEffect(targetEffect, y));
            Assert.AreEqual(true, rules.CanUseEffect(nonTargetEffect, null));
            Assert.AreEqual(false, rules.CanUseEffect(nonTargetEffect, y));

            bf[Pid.P1].Deck.Add(y);
            Assert.AreEqual(false, rules.CanUseEffect(targetEffect, null));
            Assert.AreEqual(false, rules.CanUseEffect(targetEffect, y));
            Assert.AreEqual(true, rules.CanUseEffect(nonTargetEffect, null));
            Assert.AreEqual(false, rules.CanUseEffect(nonTargetEffect, y));

            bf[Pid.P1].Deck.Remove(y);
            bf[Pid.P1].Hand.Add(y);
            Assert.AreEqual(false, rules.CanUseEffect(targetEffect, null));
            Assert.AreEqual(false, rules.CanUseEffect(targetEffect, y));
            Assert.AreEqual(true, rules.CanUseEffect(nonTargetEffect, null));
            Assert.AreEqual(false, rules.CanUseEffect(nonTargetEffect, y));

            bf[Pid.P1].Hand.Remove(y);
            bf[Pid.P2].Hand.Add(y);
            Assert.AreEqual(false, rules.CanUseEffect(targetEffect, null));
            Assert.AreEqual(false, rules.CanUseEffect(targetEffect, y));
            Assert.AreEqual(true, rules.CanUseEffect(nonTargetEffect, null));
            Assert.AreEqual(false, rules.CanUseEffect(nonTargetEffect, y));

            bf[Pid.P2].Hand.Remove(y);
            bf[Pid.P2].Field.Add(y);
            Assert.AreEqual(false, rules.CanUseEffect(targetEffect, null));
            Assert.AreEqual(true, rules.CanUseEffect(targetEffect, y));
            Assert.AreEqual(true, rules.CanUseEffect(nonTargetEffect, null));
            Assert.AreEqual(false, rules.CanUseEffect(nonTargetEffect, y));

            bf[Pid.P2].Field.Remove(y);
            bf[Pid.P1].Field.Add(y);
            Assert.AreEqual(false, rules.CanUseEffect(targetEffect, null));
            Assert.AreEqual(false, rules.CanUseEffect(targetEffect, y));
            Assert.AreEqual(true, rules.CanUseEffect(nonTargetEffect, null));
            Assert.AreEqual(false, rules.CanUseEffect(nonTargetEffect, y));
        }
    }
}
