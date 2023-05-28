using HsLib.KnownCards.Minions;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Places;

namespace HsLibTests.KnownCards.Minions
{
    [TestClass()]
    public class AbusiveSergeantTests
    {
        [TestMethod()]
        public void AbusiveSergeantTest()
        {
            Board board = TestBoard.New();

            Minion yeti = new ChillwindYeti();
            board[Pid.P1].Field.Add(yeti);
            Assert.AreEqual(4, yeti.Atk);

            Minion abusiveSergeant = new AbusiveSergeant();
            board[Pid.P1].Hand.Add(abusiveSergeant);
            Assert.AreEqual(4, yeti.Atk);

            abusiveSergeant.BattlecryEffect?.UseEffect(board, yeti)();
            Assert.AreEqual(6, yeti.Atk);

            board.Turn.Next();
            Assert.AreEqual(4, yeti.Atk);
        }
    }
}