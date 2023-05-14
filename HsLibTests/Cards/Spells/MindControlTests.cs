using HsLib.Cards.Minions;
using HsLib.Cards.Spells;
using HsLib.Systems;
using HsLib.Types;
using HsLib.Types.Cards;
using HsLibTests.Helpers;

namespace HsLibTests.Cards.Spells
{
    [TestClass()]
    public class MindControlTests
    {
        [TestMethod()]
        public void MindControlTest()
        {
            Battlefield bf = TestBattlefield.New();

            Spell mindControl = new MindControl();
            Assert.AreEqual(0, mindControl.UseEffectTargets(bf, bf.Player.Pid).Count());

            Minion yeti = new ChillwindYeti();

            bf[Pid.P1].Hand.Add(mindControl);
            Assert.AreEqual(0, mindControl.UseEffectTargets(bf, bf.Player.Pid).Count());

            bf[Pid.P1].Field.Add(yeti);
            Assert.AreEqual(0, mindControl.UseEffectTargets(bf, bf.Player.Pid).Count());

            bf[Pid.P1].Field.Remove(yeti);
            bf[Pid.P2].Field.Add(yeti);
            Assert.AreEqual(1, mindControl.UseEffectTargets(bf, bf.Player.Pid).Count());

            mindControl.UseEffect(bf, bf.Player.Pid, yeti);
            Assert.AreEqual(Pid.P1, yeti.Place?.Pid);
            Assert.AreEqual(1, bf[Pid.P1].Field.Count);
            Assert.AreEqual(0, bf[Pid.P2].Field.Count);
        }
    }
}