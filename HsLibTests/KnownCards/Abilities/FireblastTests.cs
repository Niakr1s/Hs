using HsLib.KnownCards.Abilities;
using HsLib.KnownCards.Minions;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Places;



namespace HsLibTests.KnownCards.Abilities
{
    [TestClass()]
    public class FireblastTests
    {

        [TestMethod()]
        public void FireblastTest()
        {
            Board board = TestBoard.New(p1: CardId.JainaProudmoore);
            Ability fireblast = board.Player.Ability;
            Assert.IsInstanceOfType(fireblast, typeof(Fireblast));
            Assert.AreEqual(2, fireblast.AbilityEffect.GetPossibleTargets(board).Count());

            int startMp = board.Player.Mp;

            Assert.AreEqual(30, board.Player.Hero.Hp);
            fireblast.UseAbility(board, board.Player.Hero)();
            Assert.AreEqual(29, board.Player.Hero.Hp);
            Assert.AreEqual(startMp - 2, board.Player.Mp);

            Minion y1 = new ChillwindYeti();
            Minion y2 = new ChillwindYeti();

            board[Pid.P1].Field.Add(y1);
            Assert.AreEqual(3, fireblast.AbilityEffect.GetPossibleTargets(board).Count());
            board[Pid.P2].Field.Add(y2);
            Assert.AreEqual(4, fireblast.AbilityEffect.GetPossibleTargets(board).Count());

            Assert.AreEqual(5, y1.Hp);
            Assert.AreEqual(5, y2.Hp);

            board.Turn.Skip(board.Player.Pid);
            fireblast.UseAbility(board, y1)();
            Assert.AreEqual(4, y1.Hp);

            board.Turn.Skip(board.Player.Pid);
            fireblast.UseAbility(board, y2)();
            Assert.AreEqual(4, y2.Hp);
        }
    }
}