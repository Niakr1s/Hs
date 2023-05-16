using HsLib.Cards.Abilities;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Containers.Base;
using HsLibTests.Helpers;

namespace HsLibTests.Types.Containers.Base
{
    [TestClass()]
    public class SingleContainerTests
    {
        private Battlefield _bf = null!;
        private SingleContainer<Ability> _container = null!;

        /// <summary>
        /// Creates container and fills it with <see cref="_initCards"/>.
        /// </summary>
        [TestInitialize()]
        public void ContainerTestInitialize()
        {
            _bf = TestBattlefield.New();
            _container = _bf.Player.Ability;
        }


        [TestMethod()]
        public void CardTest()
        {
            Ability ability = new Fireblast();
            _container.Card = ability;

            ContainerTestHelpers.AssertCardIsSet(_container, ability);
            Assert.AreEqual(ability, _container.Card);
        }

        [TestMethod()]
        public void SetTest()
        {
            Ability ability = new Fireblast();
            RemovedCard removedAbility = _container.Set(ability);

            ContainerTestHelpers.AssertCardIsSet(_container, ability);

            Assert.IsNotNull(removedAbility.Card);
            Assert.IsNotNull(removedAbility.Place);
        }

        [TestMethod()]
        public void InsertTest()
        {
            for (int i = -10; i < 0; i++)
            {
                Ability ability = new Fireblast();

                if (i == 0)
                {
                    _container.Insert(i, ability);

                    ContainerTestHelpers.AssertCardIsSet(_container, ability);
                }
                else
                {
                    Assert.ThrowsException<InvalidOperationException>(() => _container.Insert(i, ability));
                }
            }
        }
    }
}