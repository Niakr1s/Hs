using HsLib.KnownCards.Minions;
using HsLib.Types.Cards;
using HsLib.Types.Stats;

namespace HsLibTests.Helpers
{
    internal static class CloneTestHelpers
    {
        internal static void DoStatTest<T>(Func<Minion, Stat<int>> statChooser)
           where T : struct
        {
            Minion yeti = new ChillwindYeti();
            var yetiStat = statChooser(yeti);
            int startValue = yetiStat;

            var buff = yetiStat.AddBuff(2);
            var aura = yetiStat.AddAura(2);

            Minion clonedYeti = (Minion)yeti.Clone();
            var clonedYetiStat = statChooser(clonedYeti);

            Assert.AreEqual(startValue + 2, clonedYetiStat);

            buff.Deactivate();
            aura.Deactivate();
            Assert.AreEqual(startValue + 2, clonedYetiStat);
        }

        internal static void DoStatTest<T>(Func<Minion, Stat<bool>> statChooser)
            where T : struct
        {
            Minion yeti = new ChillwindYeti();
            var yetiStat = statChooser(yeti);
            bool startValue = yetiStat;

            var buff = yetiStat.AddBuff(true);
            var aura = yetiStat.AddAura(true);

            Minion clonedYeti = (Minion)yeti.Clone();
            var clonedYetiStat = statChooser(clonedYeti);

            Assert.AreEqual(true, clonedYetiStat);

            buff.Deactivate();
            aura.Deactivate();
            Assert.AreEqual(true, clonedYetiStat);
        }
    }
}