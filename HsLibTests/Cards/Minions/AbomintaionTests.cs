using HsLib.Cards.Minions;
using HsLib.Systems;
using HsLib.Types;
using HsLib.Types.Cards;
using HsLibTests.Helpers;

namespace HsLibTests.Cards.Minions
{
    [TestClass()]
    public class AbomintaionTests
    {
        [TestMethod()]
        public void AbomintaionTest()
        {
            Battlefield bf = TestBattlefield.New();
            Minion abom1 = new Abomintaion();
            Minion abom2 = new Abomintaion();
            Minion yeti1 = new ChillwindYeti();
            Minion yeti2 = new ChillwindYeti();

            bf[Pid.P1].Field.Add(abom1);
            bf[Pid.P2].Field.Add(abom2);
            bf[Pid.P1].Field.Add(yeti1);
            bf[Pid.P2].Field.Add(yeti2);

            bf.Turn.Skip(bf.Player.Pid);
            Assert.AreEqual(true, bf.MinionAttack(0, Loc.Field, 0));
            Assert.AreEqual(0, abom1.Hp);
            Assert.AreEqual(0, abom1.Hp);
            Assert.AreEqual(1, yeti1.Hp);
            Assert.AreEqual(1, yeti2.Hp);
        }
    }
}