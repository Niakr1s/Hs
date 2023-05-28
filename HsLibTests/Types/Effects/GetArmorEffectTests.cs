using HsLib.KnownCards.Minions;

using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Effects;

namespace HsLibTests.Types.Effects
{
    [TestClass()]
    public class GetArmorEffectTests
    {
        [TestMethod()]
        public void UseEffectTest_ShouldWorkOnHero()
        {
            Battlefield bf = TestBattlefield.New();
            GetArmorEffect effect = new() { Armor = 2 };

            Hero hero = bf.Enemy.Hero;
            int startArmor = hero.Armor;
            effect.UseEffect(bf, null!, hero)();
            Assert.AreEqual(startArmor + 2, hero.Armor);
        }

        [TestMethod()]
        public void UseEffectTest_ShouldntWorkOnNonHero()
        {
            Battlefield bf = TestBattlefield.New();
            GetArmorEffect effect = new() { Armor = 2 };

            Minion minion = new ChillwindYeti();
            Assert.ThrowsException<ValidationException>(() => effect.UseEffect(bf, null!, minion));
        }
    }
}