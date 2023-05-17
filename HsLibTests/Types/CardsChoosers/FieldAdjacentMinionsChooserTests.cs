using HsLib.Cards.Minions;
using HsLib.Systems;
using HsLib.Types;
using HsLib.Types.Cards;
using HsLib.Types.CardsChoosers;
using HsLibTests.Helpers;

namespace HsLibTests.Types.CardsChoosers
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
            PlaceInContainer place = new(Pid.P1, Loc.Field, 0, 0);

            for (int i = 0; i < _minions.Count; i++)
            {
                int expected = i == 0 || i == _minions.Count - 1 ? 1 : 2;
                Assert.AreEqual(expected, chooser.ChooseCards(place with { Index = i }, _bf.Cards).Count());
            }
        }
    }
}