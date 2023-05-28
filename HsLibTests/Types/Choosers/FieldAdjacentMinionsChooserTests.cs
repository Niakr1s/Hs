using HsLib.KnownCards.Minions;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Choosers;

namespace HsLibTests.Types.Choosers
{
    [TestClass()]
    public class FieldAdjacentMinionsChooserTests
    {
        Board _board = null!;
        List<Minion> _minions = null!;
        Minion _playerHandMinion = null!;
        Minion _enemyFieldMinion = null!;

        [TestInitialize()]
        public void Setup()
        {
            _board = TestBoard.New();
            _minions = new()
            {
                new ChillwindYeti(),
                new ChillwindYeti(),
                new ChillwindYeti(),
            };
            _minions.ForEach(_board.Player.Field.Add);

            _playerHandMinion = new ChillwindYeti();
            _board.Player.Hand.Add(_playerHandMinion);

            _enemyFieldMinion = new ChillwindYeti();
            _board.Enemy.Field.Add(_enemyFieldMinion);
        }

        [TestMethod()]
        public void ChooseCards()
        {
            FieldAdjacentMinionsChooser chooser = new();

            for (int i = 0; i < _minions.Count; i++)
            {
                int expected = i == 0 || i == _minions.Count - 1 ? 1 : 2;
                Assert.AreEqual(expected, chooser.ChooseCards(_board, _minions[i]).Count());
            }
        }
    }
}