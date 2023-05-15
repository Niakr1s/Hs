﻿using HsLib.Cards.Abilities;
using HsLib.Cards.Minions;
using HsLib.Systems;
using HsLib.Types;
using HsLib.Types.Cards;
using HsLib.Types.Containers;
using HsLibTests.Helpers;

namespace HsLibTests.Cards.Abilities
{
    [TestClass()]
    public class FireblastTests
    {

        [TestMethod()]
        public void FireblastTest()
        {
            Battlefield bf = TestBattlefield.New(p1: CardId.JainaProudmoore);
            AbilityContainer ability = bf.Player.Ability;
            Ability fireblast = ability.Card;
            Assert.IsInstanceOfType(fireblast, typeof(Fireblast));
            Assert.AreEqual(2, fireblast.AbilityEffect.GetPossibleTargets(bf, bf.Player.Pid).Count());

            int startMp = bf.Player.Mp;

            Assert.AreEqual(30, bf.Player.Hero.Card.Hp);
            ability.UseAbility(bf.Player.Hero.Card)();
            Assert.AreEqual(29, bf.Player.Hero.Card.Hp);
            Assert.AreEqual(startMp - 2, bf.Player.Mp);

            Minion y1 = new ChillwindYeti();
            Minion y2 = new ChillwindYeti();

            bf[Pid.P1].Field.Add(y1);
            Assert.AreEqual(3, fireblast.AbilityEffect.GetPossibleTargets(bf, bf.Player.Pid).Count());
            bf[Pid.P2].Field.Add(y2);
            Assert.AreEqual(4, fireblast.AbilityEffect.GetPossibleTargets(bf, bf.Player.Pid).Count());

            Assert.AreEqual(5, y1.Hp);
            Assert.AreEqual(5, y2.Hp);

            bf.Turn.Skip(bf.Player.Pid);
            ability.UseAbility(y1)();
            Assert.AreEqual(4, y1.Hp);

            bf.Turn.Skip(bf.Player.Pid);
            ability.UseAbility(y2)();
            Assert.AreEqual(4, y2.Hp);
        }
    }
}