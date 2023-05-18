using HsLib.KnownCards.Minions;
using HsLib.KnownCards.Spells;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLibTests.Helpers;

namespace HsLibTests.KnownCards.Spells
{
    [TestClass()]
    public class HolySmiteTests
    {
        [TestMethod()]
        public void HolySmiteTest()
        {
            Battlefield bf = TestBattlefield.New();

            Spell holySmite = new HolySmite();
            bf.Player.Hand.Add(holySmite);
            Assert.AreEqual(2, holySmite.SpellEffect.GetPossibleTargets(bf, holySmite.PlaceInContainer!.Pid).Count()); // mine and his heros

            Minion yeti = new ChillwindYeti();
            bf.Enemy.Field.Add(yeti);
            Assert.AreEqual(3, holySmite.SpellEffect.GetPossibleTargets(bf, holySmite.PlaceInContainer!.Pid).Count());

            holySmite.SpellEffect.UseEffect(bf, holySmite.PlaceInContainer!.Pid, yeti)();
            Assert.AreEqual(3, yeti.Hp);
        }
    }
}