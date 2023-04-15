using HsLib.Common.Place;
using HsLib.Containers;

namespace HsLib.Cards.KnownCards.Minions.Tests
{
    [TestClass()]
    public class AbusiveSergeantTests
    {
        [TestMethod()]
        public void AbusiveSergeantTest()
        {
            Battlefield bf = new Battlefield(CardId.JainaProudmoore, CardId.JainaProudmoore);
            bf.Turn.Next();

            Minion yeti = new ChillwindYeti();
            bf[Pid.P1].Field.Add(yeti);
            Assert.AreEqual(4, yeti.Atk.Value);

            Minion abusiveSergeant = new AbusiveSergeant();
            bf[Pid.P1].Hand.Add(abusiveSergeant);
            Assert.AreEqual(4, yeti.Atk.Value);

            abusiveSergeant.Battlecry?.UseEffect(bf, abusiveSergeant, yeti);
            Assert.AreEqual(6, yeti.Atk.Value);

            bf.Turn.Next();
            Assert.AreEqual(4, yeti.Atk.Value);
        }
    }
}