using HsLib.KnownCards.Minions;

using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Effects;

namespace HsLibTests.Types.Effects
{
    [TestClass()]
    public class MindControlEffectTests
    {
        Board _board = null!;
        MindControlEffect _effect = null!;

        [TestInitialize()]
        public void MindControlEffectTestsInitialize()
        {
            _board = TestBoard.New();
            _effect = new();
        }

        [TestMethod()]
        public void UseEffectTest_ShouldWorkOnFieldMinion()
        {
            Minion minion = new ChillwindYeti();

            _board.Enemy.Field.Add(minion);
            _effect.UseEffect(_board, null!, minion)();

            Assert.AreEqual(0, _board.Enemy.Field.Count);
            Assert.AreEqual(1, _board.Player.Field.Count);
            Assert.AreEqual(minion, _board.Player.Field[0]);
        }

        [TestMethod()]
        public void UseEffectTest_ShouldNotWorkOnNonFieldMinion()
        {
            Minion minion = new ChillwindYeti();

            _board.Enemy.Hand.Add(minion);
            Assert.ThrowsException<ValidationException>(() => _effect.UseEffect(_board, null!, minion));

            Assert.AreEqual(1, _board.Enemy.Hand.Count);
            Assert.AreEqual(0, _board.Player.Field.Count);
            Assert.AreEqual(minion, _board.Enemy.Hand[0]);
        }
    }
}