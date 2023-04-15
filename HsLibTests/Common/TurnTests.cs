﻿using HsLib.Common.Place;

namespace HsLib.Common.Tests
{
    [TestClass()]
    public class TurnTests
    {
        [TestMethod()]
        public void NextTest()
        {
            Turn t = new Turn();
            Assert.AreEqual(0, t.No);
            Assert.AreEqual(Pid.None, t.Pid);

            t.Next();
            Assert.AreEqual(1, t.No);
            Assert.AreEqual(Pid.P1, t.Pid);

            t.Next();
            Assert.AreEqual(2, t.No);
            Assert.AreEqual(Pid.P2, t.Pid);
        }

        [TestMethod()]
        public void SkipPidTest()
        {
            Turn t = new Turn();

            Assert.ThrowsException<Exception>(() => t.Skip(Pid.None));

            t.Skip(Pid.P1);
            Assert.AreEqual(1, t.No);

            t.Skip(Pid.P2);
            Assert.AreEqual(2, t.No);

            t.Skip(Pid.P2);
            Assert.AreEqual(4, t.No);
        }

        [TestMethod()]
        public void SkipTurnsTest()
        {
            Turn t = new Turn();
            Assert.AreEqual(0, t.No);

            t.Skip(3);
            Assert.AreEqual(3, t.No);
        }
    }
}