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
            Assert.AreEqual(0, mindControl.SpellEffect.GetPossibleTargets(bf, Pid.P1).Count());

            Minion yeti = new ChillwindYeti();

            bf[Pid.P1].Hand.Add(mindControl);
            Assert.AreEqual(0, mindControl.SpellEffect.GetPossibleTargets(bf, Pid.P1).Count());

            bf[Pid.P1].Field.Add(yeti);
            Assert.AreEqual(0, mindControl.SpellEffect.GetPossibleTargets(bf, Pid.P1).Count());

            bf[Pid.P1].Field.Remove(yeti);
            bf[Pid.P2].Field.Add(yeti);
            Assert.AreEqual(1, mindControl.SpellEffect.GetPossibleTargets(bf, Pid.P1).Count());

            mindControl.SpellEffect.UseEffect(bf, mindControl.PlaceInContainer!.Pid, yeti)();
            Assert.AreEqual(Pid.P1, yeti.PlaceInContainer?.Pid);
            Assert.AreEqual(1, bf[Pid.P1].Field.Count);
            Assert.AreEqual(0, bf[Pid.P2].Field.Count);
        }
    }
}