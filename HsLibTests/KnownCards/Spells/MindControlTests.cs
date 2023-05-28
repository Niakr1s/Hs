using HsLib.KnownCards.Minions;
using HsLib.KnownCards.Spells;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Places;

namespace HsLibTests.KnownCards.Spells
{
    [TestClass()]
    public class MindControlTests
    {
        [TestMethod()]
        public void MindControlTest()
        {
            Board board = TestBoard.New();

            Spell mindControl = new MindControl();
            Assert.AreEqual(0, mindControl.SpellEffect.GetPossibleTargets(board).Count());

            Minion yeti = new ChillwindYeti();

            board[Pid.P1].Hand.Add(mindControl);
            Assert.AreEqual(0, mindControl.SpellEffect.GetPossibleTargets(board).Count());

            board[Pid.P1].Field.Add(yeti);
            Assert.AreEqual(0, mindControl.SpellEffect.GetPossibleTargets(board).Count());

            board[Pid.P1].Field.Remove(yeti);
            board[Pid.P2].Field.Add(yeti);
            Assert.AreEqual(1, mindControl.SpellEffect.GetPossibleTargets(board).Count());

            mindControl.SpellEffect.UseEffect(board, yeti)();
            Assert.AreEqual(Pid.P1, yeti.Place.Pid);
            Assert.AreEqual(1, board[Pid.P1].Field.Count);
            Assert.AreEqual(0, board[Pid.P2].Field.Count);
        }
    }
}