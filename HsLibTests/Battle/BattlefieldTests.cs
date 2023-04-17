using HsLib.Cards;
using HsLib.Cards.KnownCards.Minions;
using HsLib.Common.Place;

namespace HsLib.Battle.Tests
{
    [TestClass()]
    public class BattlefieldTests
    {
        [TestMethod()]
        public void BattlefieldCardsTest()
        {
            Battlefield bf = new Battlefield(CardId.JainaProudmoore, CardId.JainaProudmoore);

            Minion yeti = new ChillwindYeti();

            // 2x: Hero, Ability, Weapon (NoWeapon)
            Assert.AreEqual(6, bf.Cards.Count());
            bf[Pid.P1].Field.Add(yeti);

            Assert.AreEqual(7, bf.Cards.Count());
        }
    }
}