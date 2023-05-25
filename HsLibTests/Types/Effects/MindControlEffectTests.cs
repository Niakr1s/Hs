using HsLib.KnownCards.Minions;

using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Effects;

namespace HsLibTests.Types.Effects
{
    [TestClass()]
    public class MindControlEffectTests
    {
        Battlefield _bf = null!;
        MindControlEffect _effect = null!;

        [TestInitialize()]
        public void MindControlEffectTestsInitialize()
        {
            _bf = TestBattlefield.New();
            _effect = new();
        }

        [TestMethod()]
        public void UseEffectTest_ShouldWorkOnFieldMinion()
        {
            Minion minion = new ChillwindYeti();

            _bf.Enemy.Field.Add(minion);
            _effect.UseEffect(_bf, null!, minion)();

            Assert.AreEqual(0, _bf.Enemy.Field.Count);
            Assert.AreEqual(1, _bf.Player.Field.Count);
            Assert.AreEqual(minion, _bf.Player.Field[0]);
        }

        [TestMethod()]
        public void UseEffectTest_ShouldNotWorkOnNonFieldMinion()
        {
            Minion minion = new ChillwindYeti();

            _bf.Enemy.Hand.Add(minion);
            Assert.ThrowsException<ValidationException>(() => _effect.UseEffect(_bf, null!, minion));

            Assert.AreEqual(1, _bf.Enemy.Hand.Count);
            Assert.AreEqual(0, _bf.Player.Field.Count);
            Assert.AreEqual(minion, _bf.Enemy.Hand[0]);
        }
    }
}