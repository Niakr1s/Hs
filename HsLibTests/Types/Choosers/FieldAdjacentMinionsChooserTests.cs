using HsLib.KnownCards.Minions;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Choosers;
using HsLib.Types.Places;

namespace HsLibTests.Types.Choosers
{
    [TestClass()]
    public class FieldAdjacentMinionsChooserTests
    {
        Battlefield _bf = null!;
        List<Minion> _minions = null!;
        Minion _playerHandMinion = null!;
        Minion _enemyFieldMinion = null!;

        [TestInitialize()]
        public void Setup()
        {
            _bf = TestBattlefield.New();
            _minions = new()
            {
                new ChillwindYeti(),
                new ChillwindYeti(),
                new ChillwindYeti(),
            };
            _minions.ForEach(_bf.Player.Field.Add);

            _playerHandMinion = new ChillwindYeti();
            _bf.Player.Hand.Add(_playerHandMinion);

            _enemyFieldMinion = new ChillwindYeti();
            _bf.Enemy.Field.Add(_enemyFieldMinion);
        }

        [TestMethod()]
        public void ChooseCards()
        {
            FieldAdjacentMinionsChooser chooser = new();
            Place place = new(Pid.P1, Loc.Field);

            for (int i = 0; i < _minions.Count; i++)
            {
                int expected = i == 0 || i == _minions.Count - 1 ? 1 : 2;
                Assert.AreEqual(expected, chooser.ChooseCards(_bf, _minions[0]).Count());
            }
        }
    }
}