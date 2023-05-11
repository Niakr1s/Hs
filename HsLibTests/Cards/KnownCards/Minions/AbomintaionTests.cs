using HsLib.Battle;
using HsLib.Common.Place;

namespace HsLib.Cards.KnownCards.Minions.Tests
{
    [TestClass()]
    public class AbomintaionTests
    {
        [TestMethod()]
        public void AbomintaionTest()
        {
            Battlefield bf = new Battlefield(CardId.JainaProudmoore, CardId.JainaProudmoore);
            Minion abom1 = new Abomintaion();
            Minion abom2 = new Abomintaion();
            Minion yeti1 = new ChillwindYeti();
            Minion yeti2 = new ChillwindYeti();

            bf[Pid.P1].Field.Add(abom1);
            bf[Pid.P2].Field.Add(abom2);
            bf[Pid.P1].Field.Add(yeti1);
            bf[Pid.P2].Field.Add(yeti2);

            bf.BattleService.WithRules = false;
            bf.BattleService.BSRules = null;
            Assert.AreEqual(true, bf.BattleService.MeleeAttack(abom1, abom2));
            Assert.AreEqual(0, abom1.Hp);
            Assert.AreEqual(0, abom1.Hp);
            Assert.AreEqual(1, yeti1.Hp);
            Assert.AreEqual(1, yeti2.Hp);
        }
    }
}