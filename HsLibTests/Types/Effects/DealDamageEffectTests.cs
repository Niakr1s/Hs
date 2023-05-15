using HsLib.Cards.Minions;
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
            DealDamageEffect effect = new() { Damage = 2 };
            Minion minion = new ChillwindYeti();

            minion.Hp.Set(10);
            // todo
            //effect.UseEffect()
        }
    }
}
