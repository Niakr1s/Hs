using HsLib.Cards.Minions;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Effects;
using HsLibTests.Helpers;

namespace HsLibTests.Types.Effects
{
    [TestClass()]
    public class DealDamageEffectTests
    {
        [TestMethod()]
        public void UseEffectTest()
        {
            Battlefield bf = TestBattlefield.New();

            DealDamageEffect effect = new() { Damage = 2 };

            Minion minion = new ChillwindYeti();
            int startHp = minion.Hp;
            effect.UseEffect(bf, minion)();
            Assert.AreEqual(startHp - effect.Damage, minion.Hp);
        }
    }
}
