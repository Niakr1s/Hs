using HsLib.KnownCards.Minions;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Places;

namespace HsLibTests.KnownCards.Minions
{
    [TestClass()]
    public class AbusiveSergeantTests
    {
        [TestMethod()]
        public void AbusiveSergeantTest()
        {
            Battlefield bf = TestBattlefield.New();

            Minion yeti = new ChillwindYeti();
            bf[Pid.P1].Field.Add(yeti);
            Assert.AreEqual(4, yeti.Atk);

            Minion abusiveSergeant = new AbusiveSergeant();
            bf[Pid.P1].Hand.Add(abusiveSergeant);
            Assert.AreEqual(4, yeti.Atk);

            abusiveSergeant.BattlecryEffect?.UseEffect(bf, yeti)();
            Assert.AreEqual(6, yeti.Atk);

            bf.Turn.Next();
            Assert.AreEqual(4, yeti.Atk);
        }
    }
}