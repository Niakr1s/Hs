using HsLib.KnownCards.Abilities;
using HsLib.KnownCards.Minions;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Places;

namespace HsLibTests.KnownCards.Abilities
{
    [TestClass()]
    public class ArmorUpTests
    {
        [TestMethod()]
        public void ArmorUpTest()
        {
            Board board = TestBoard.New(p1: CardId.GarroshHellscream);
            Ability armorUp = board.Player.Ability;
            Assert.IsInstanceOfType(armorUp, typeof(ArmorUp));
            Assert.AreEqual(0, armorUp.AbilityEffect.GetPossibleTargets(board).Count());


            Assert.AreEqual(0, board.Player.Hero.Armor);
            Assert.AreEqual(true, board.UseAbility());
            Assert.AreEqual(2, board.Player.Hero.Armor);

            Minion y1 = new ChillwindYeti();
            board[Pid.P1].Field.Add(y1);
            Assert.AreEqual(0, armorUp.AbilityEffect.GetPossibleTargets(board).Count());
        }
    }
}