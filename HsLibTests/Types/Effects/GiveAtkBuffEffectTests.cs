using HsLib.KnownCards.Minions;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Effects;

namespace HsLibTests.Types.Effects
{
    [TestClass()]
    public class GiveStatBuffEffectTests
    {
        [TestMethod()]
        public void UseEffectTest()
        {
            DoUseEffectTest(false);
            DoUseEffectTest(true);
        }

        private static void DoUseEffectTest(bool tillEndOfTurn)
        {
            Battlefield bf = TestBattlefield.New();

            GiveStatBuffEffect<int> effect = new(c => ((IWithAtk)c).Atk) { Value = 2, TillEndOfTurn = tillEndOfTurn };

            Minion minion = new ChillwindYeti();
            int startAtk = minion.Atk;
            effect.UseEffect(bf, null!, minion)();

            int expectedAtk = startAtk + effect.Value;
            Assert.AreEqual(expectedAtk, minion.Atk);

            bf.Turn.Next();
            if (tillEndOfTurn) { expectedAtk = startAtk; }
            Assert.AreEqual(expectedAtk, minion.Atk);
        }
    }
}