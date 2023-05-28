using HsLib.KnownCards.Minions;
using HsLib.KnownCards.Spells;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Places;

namespace HsLibTests.KnownCards.Spells
{
    [TestClass()]
    public class MindControlTests
    {
        [TestMethod()]
        public void MindControlTest()
        {
            Battlefield bf = TestBattlefield.New();

            Spell mindControl = new MindControl();
            Assert.AreEqual(0, mindControl.SpellEffect.GetPossibleTargets(bf).Count());

            Minion yeti = new ChillwindYeti();

            bf[Pid.P1].Hand.Add(mindControl);
            Assert.AreEqual(0, mindControl.SpellEffect.GetPossibleTargets(bf).Count());

            bf[Pid.P1].Field.Add(yeti);
            Assert.AreEqual(0, mindControl.SpellEffect.GetPossibleTargets(bf).Count());

            bf[Pid.P1].Field.Remove(yeti);
            bf[Pid.P2].Field.Add(yeti);
            Assert.AreEqual(1, mindControl.SpellEffect.GetPossibleTargets(bf).Count());

            mindControl.SpellEffect.UseEffect(bf, yeti)();
            Assert.AreEqual(Pid.P1, yeti.Place.Pid);
            Assert.AreEqual(1, bf[Pid.P1].Field.Count);
            Assert.AreEqual(0, bf[Pid.P2].Field.Count);
        }
    }
}