﻿using HsLib.Cards.Minions;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Effects;
using HsLibTests.Helpers;

namespace HsLibTests.Types.Effects
{
    [TestClass()]
    public class GiveAtkBuffEffectTests
    {
        [TestMethod()]
        public void UseEffectTest()
        {
            DoUseEffectTest(false);
            DoUseEffectTest(true);
        }

        private static void DoUseEffectTest(bool tillEndOfTurn)
        {
            Battlefield bf = TestBattlefield.New();

            GiveAtkBuffEffect effect = new() { DamageBuff = 2, TillEndOfTurn = tillEndOfTurn };

            Minion minion = new ChillwindYeti();
            int startAtk = minion.Atk;
            effect.UseEffect(bf, minion)();

            int expectedAtk = startAtk + effect.DamageBuff;
            Assert.AreEqual(expectedAtk, minion.Atk);

            bf.Turn.Next();
            if (tillEndOfTurn) { expectedAtk = startAtk; }
            Assert.AreEqual(expectedAtk, minion.Atk);
        }
    }
}