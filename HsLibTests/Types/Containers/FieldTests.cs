using HsLib.KnownCards.Minions;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Containers;
using HsLibTests.Helpers;

namespace HsLibTests.Types.Containers
{
    [TestClass()]
    public class FieldTests
    {
        private Battlefield _bf = null!;

        /// <summary>
        /// Inits with minion x 3.
        /// </summary>
        private Field _field = null!;
        private readonly List<Minion> _initCards =
            new() { new ChillwindYeti(), new ChillwindYeti(), new ChillwindYeti() };

        /// <summary>
        /// Creates container and fills it with <see cref="_initCards"/>.
        /// </summary>
        [TestInitialize()]
        public void ContainerTestInitialize()
        {
            _bf = TestBattlefield.New();
            _field = _bf.Player.Field;

            foreach (Minion card in _initCards)
            {
                _field.Add(card);
            }
        }

        [TestMethod()]
        public void Limit()
        {
            Assert.AreEqual(7, _field.Limit);
        }

        [TestMethod()]
        public void HasAnyActiveTauntTest()
        {
            Assert.AreEqual(false, _field.HasAnyActiveTaunt());

            _field[0].Taunt.Set(true);
            Assert.AreEqual(true, _field.HasAnyActiveTaunt());
        }
    }
}