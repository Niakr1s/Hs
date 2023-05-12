using HsLib.Battle;
using HsLib.Cards.KnownCards.Minions;
using HsLib.Common.Place;

namespace HsLib.Cards.KnownCards.Spells.Tests
{
    [TestClass()]
    public class MindControlTests
    {
        [TestMethod()]
        public void MindControlTest()
        {
            Battlefield bf = new Battlefield(CardId.JainaProudmoore, CardId.JainaProudmoore);
            bf.Turn.Start();

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
        [TestMethod()]
        public void MindControlCanUseEffectTest()
        {
            Battlefield bf = new Battlefield(CardId.JainaProudmoore, CardId.JainaProudmoore);
            bf.Turn.Start();

            Spell mindControl = new MindControl();
            Assert.AreEqual(false, mindControl.CanUseEffect(bf));
            Assert.AreEqual(0, mindControl.UseEffectTargets(bf).Count());

            Minion yeti = new ChillwindYeti();

            bf[Pid.P1].Hand.Add(mindControl);
            Assert.AreEqual(false, mindControl.CanUseEffect(bf));
            Assert.AreEqual(0, mindControl.UseEffectTargets(bf).Count());

            bf[Pid.P1].Field.Add(yeti);
            Assert.AreEqual(false, mindControl.CanUseEffect(bf));
            Assert.AreEqual(0, mindControl.UseEffectTargets(bf).Count());

            bf[Pid.P1].Field.Remove(yeti);
            bf[Pid.P2].Field.Add(yeti);
            Assert.AreEqual(false, mindControl.CanUseEffect(bf));
            Assert.AreEqual(1, mindControl.UseEffectTargets(bf).Count());

            bf[Pid.P1].Mp.Set(10);
            Assert.AreEqual(false, mindControl.CanUseEffect(bf));
            Assert.AreEqual(1, mindControl.UseEffectTargets(bf).Count());

            bf.Turn.Next();
            Assert.AreEqual(true, mindControl.CanUseEffect(bf));
            Assert.AreEqual(1, mindControl.UseEffectTargets(bf).Count());
        }
    }
}