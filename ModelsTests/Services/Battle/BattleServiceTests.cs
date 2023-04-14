using Models.Cards;
using Models.Cards.KnownCards.Minions;
using Models.Common;
using Models.Containers;

namespace Models.Services.Battle.Tests
{
    [TestClass()]
    public class BattleServiceTests
    {
        [TestMethod()]
        public void MinionAttackTest()
        {
            Battlefield bf = new Battlefield(HeroId.Jaina, HeroId.Rexxar);
            Minion yeti1 = new ChillwindYeti();
            Minion yeti2 = new ChillwindYeti();

            bf[Pid.P1].Deck.Add(yeti1);
            bf[Pid.P2].Deck.Add(yeti2);

            bf.BattleService.MinionAttack(yeti1, yeti2);
            Assert.AreEqual(1, yeti1.Hp.Value);
            Assert.AreEqual(1, yeti2.Hp.Value);

            bf.BattleService.MinionAttack(yeti1, yeti2);
            Assert.AreEqual(1, yeti1.Hp.Value);
            Assert.AreEqual(0, yeti2.Hp.Value);
        }
    }
}