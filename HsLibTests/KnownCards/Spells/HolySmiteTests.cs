using HsLib.KnownCards.Minions;
using HsLib.KnownCards.Spells;
using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLibTests.KnownCards.Spells
{
    [TestClass()]
    public class HolySmiteTests
    {
        [TestMethod()]
        public void HolySmiteTest()
        {
            Board board = TestBoard.New();

            Spell holySmite = new HolySmite();
            board.Player.Hand.Add(holySmite);
            Assert.AreEqual(2, holySmite.SpellEffect.GetPossibleTargets(board).Count()); // mine and his heros

            Minion yeti = new ChillwindYeti();
            board.Enemy.Field.Add(yeti);
            Assert.AreEqual(3, holySmite.SpellEffect.GetPossibleTargets(board).Count());

            holySmite.SpellEffect.UseEffect(board, yeti)();
            Assert.AreEqual(3, yeti.Hp);
        }
    }
}