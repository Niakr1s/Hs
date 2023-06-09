﻿using HsLib.Types.Stats;

namespace HsLibTests.Types.Stats
{
    [TestClass()]
    public class IntStatTests
    {
        [TestMethod()]
        public void DecreaseTest()
        {
            Hp hp = new(30);

            StatDecreasedEventArgs? eventArgs = null;
            hp.Decreased += (s, e) => eventArgs = e;

            hp.Decrease(5);
            Assert.AreEqual(25, hp);
            Assert.IsNotNull(eventArgs);
            Assert.AreEqual(5, eventArgs.Amount);
        }

        [TestMethod()]
        public void DecreaseTest_NegativeValue()
        {
            Hp hp = new(30);
            Assert.ThrowsException<ArgumentException>(() => hp.Decrease(-1));
        }

        [TestMethod()]
        public void IncreaseTest()
        {
            Hp hp = new(30);

            StatIncreasedEventArgs? eventArgs = null;
            hp.Increased += (s, e) => eventArgs = e;

            hp.Increase(5);
            Assert.AreEqual(35, hp);
            Assert.IsNotNull(eventArgs);
            Assert.AreEqual(5, eventArgs.Amount);
        }

        [TestMethod()]
        public void IncreaseTest_NegativeValue()
        {
            Hp hp = new(30);
            Assert.ThrowsException<ArgumentException>(() => hp.Increase(-1));
        }

        [TestMethod()]
        public void CloneTest()
        {
            IntStat stat = new(3);
            stat.AddFinalMultiplierAura(3);

            IntStat cloned = (IntStat)stat.Clone();
            Assert.AreEqual(3, cloned);
        }
    }
}