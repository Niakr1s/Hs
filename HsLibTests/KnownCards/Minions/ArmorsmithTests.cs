﻿using HsLib.KnownCards.Minions;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Containers;
using HsLib.Types.Places;

namespace HsLibTests.KnownCards.Minions
{
    [TestClass()]
    public class ArmorsmithTests
    {
        [TestMethod()]
        public void ArmorsmithTest()
        {
            Board board = TestBoard.New();

            Minion armorsmith = new Armorsmith();
            Minion y1 = new ChillwindYeti();
            Minion y2 = new ChillwindYeti();
            Minion enemyYeti = new ChillwindYeti();

            Field playerField = board[Pid.P1].Field;
            Field enemyField = board[Pid.P2].Field;

            enemyField.Add(enemyYeti);
            playerField.Add(y1);
            playerField.Add(y2);
            playerField.Add(armorsmith);

            var playerArmor = board.Player.Hero.Armor;
            int expectedArmor = playerArmor;

            enemyYeti.Hp.Decrease(1);
            Assert.AreEqual(expectedArmor, playerArmor);

            y1.Hp.Decrease(1);
            Assert.AreEqual(++expectedArmor, playerArmor);

            y2.Hp.Decrease(1);
            Assert.AreEqual(++expectedArmor, playerArmor);

            Minion y3 = new ChillwindYeti();
            playerField.Add(y3);
            y3.Hp.Decrease(1);
            Assert.AreEqual(++expectedArmor, playerArmor);

            board.Enemy.Hero.Hp.Decrease(1);
            Assert.AreEqual(expectedArmor, playerArmor);

            // removing y2 and trying to damage it
            playerField.Remove(y2);

            y2.Hp.Decrease(1);
            Assert.AreEqual(expectedArmor, playerArmor);

            // removing armorsmith and trying to damage remaining y1
            playerField.Remove(armorsmith);
            y1.Hp.Decrease(1);
            Assert.AreEqual(expectedArmor, playerArmor);

            // shouldn't work at hand
            board.Player.Hand.Add(armorsmith);
            y1.Hp.Decrease(1);
            Assert.AreEqual(expectedArmor, playerArmor);

            // not even with new minion
            Minion y4 = new ChillwindYeti();
            playerField.Add(y4);
            y4.Hp.Decrease(1);
            Assert.AreEqual(expectedArmor, playerArmor);
        }
    }
}