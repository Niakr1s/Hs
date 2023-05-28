using HsLib.KnownCards.Minions;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Effects;

namespace HsLibTests.Types.Effects
{
    [TestClass()]
    public class DealDamageEffectTests
    {
        [TestMethod()]
        public void UseEffectTest()
        {
            Board board = TestBoard.New();

            DamageEffect effect = new(2);

            Minion minion = new ChillwindYeti();
            int startHp = minion.Hp;
            effect.UseEffect(board, null!, minion)();
            Assert.AreEqual(startHp - effect.DamageAmount, minion.Hp);
        }
    }
}
