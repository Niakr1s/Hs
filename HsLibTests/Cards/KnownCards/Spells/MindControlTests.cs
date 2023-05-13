using HsLib.Battle;
using HsLib.Cards.KnownCards.Minions;
using HsLib.Common.Place;
using HsLibTests;

namespace HsLib.Cards.KnownCards.Spells.Tests
{
    [TestClass()]
    public class MindControlTests
    {
        [TestMethod()]
        public void MindControlTest()
        {
            Battlefield bf = TestBattlefield.New();

            Spell mindControl = new MindControl();
            Assert.AreEqual(0, mindControl.UseEffectTargets(bf).Count());

            Minion yeti = new ChillwindYeti();

            bf[Pid.P1].Hand.Add(mindControl);
            Assert.AreEqual(0, mindControl.UseEffectTargets(bf).Count());

            bf[Pid.P1].Field.Add(yeti);
            Assert.AreEqual(0, mindControl.UseEffectTargets(bf).Count());

            bf[Pid.P1].Field.Remove(yeti);
            bf[Pid.P2].Field.Add(yeti);
            Assert.AreEqual(1, mindControl.UseEffectTargets(bf).Count());

            mindControl.UseEffect(bf, yeti);
            Assert.AreEqual(Pid.P1, yeti.Pid);
            Assert.AreEqual(1, bf[Pid.P1].Field.Count);
            Assert.AreEqual(0, bf[Pid.P2].Field.Count);
        }
    }
}