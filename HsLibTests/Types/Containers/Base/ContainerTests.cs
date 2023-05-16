using HsLib.Cards.Minions;
using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Containers.Base;
using HsLibTests.Helpers;

namespace HsLibTests.Types.Containers.Base
{
    [TestClass()]
    public class ContainerTests
    {
        private Battlefield _bf = null!;

        /// <summary>
        /// Inits with minion x 3.
        /// </summary>
        private Container<Minion> _container = null!;
        private readonly List<Minion> _initCards =
            new() { new ChillwindYeti(), new ChillwindYeti(), new ChillwindYeti() };

        /// <summary>
        /// Creates container and fills it with <see cref="_initCards"/>.
        /// </summary>
        [TestInitialize()]
        public void ContainerTestInitialize()
        {
            _bf = TestBattlefield.New();
            _container = _bf.Player.Field;

            foreach (Minion card in _initCards)
            {
                _container.Add(card);
            }
        }

        [TestMethod()]
        public void Constructor_StartCardsTest()
        {
            Container<Minion> container = new(_bf, new HsLib.Types.Place(HsLib.Types.Pid.P1, HsLib.Types.Loc.Field), startCards: _initCards);
            Assert.AreEqual(_initCards.Count, container.Count);
            ContainerTestHelpers.AssertCardsHaveValidPlaces(container);
        }

        [TestMethod()]
        public void Constructor_LimitTest()
        {
            const int limit = 2;
            Container<Minion> container = new(_bf, new HsLib.Types.Place(HsLib.Types.Pid.P1, HsLib.Types.Loc.Field), limit: limit);

            for (int i = 0; i < limit; i++)
            {
                container.Add(new ChillwindYeti());
                ContainerTestHelpers.AssertCardsHaveValidPlaces(container);
            }
            Assert.AreEqual(limit, container.Count);

            Assert.ThrowsException<InvalidOperationException>(() => container.Add(new ChillwindYeti()));
            ContainerTestHelpers.AssertCardsHaveValidPlaces(container);
            Assert.AreEqual(limit, container.Count);
        }

        [TestMethod()]
        public void AddTest()
        {
            int startCount = _container.Count;
            _container.Add(new ChillwindYeti());
            ContainerTestHelpers.AssertCardsHaveValidPlaces(_container);
            Assert.AreEqual(startCount + 1, _container.Count);
        }

        [TestMethod()]
        public void InsertTest()
        {
            int startCount = _container.Count;
            for (int i = _container.Count; i >= 0; i--)
            {
                Minion m = new ChillwindYeti();
                _container.Insert(i, m);
                ContainerTestHelpers.AssertCardsHaveValidPlaces(_container);

                Assert.AreEqual(_container.Place, m.Place!);
                Assert.AreEqual(++startCount, _container.Count);
            }
            Assert.ThrowsException<InvalidOperationException>(() => _container.Insert(_container.Count + 1, new ChillwindYeti()));
            Assert.ThrowsException<InvalidOperationException>(() => _container.Insert(-1, new ChillwindYeti()));
        }

        [TestMethod()]
        public void PopTest()
        {
            for (int i = _initCards.Count - 1; i >= 0; i--)
            {
                Minion? removedCard = _container.Pop();
                ContainerTestHelpers.AssertCardsHaveValidPlaces(_container);
                Assert.IsNotNull(removedCard);
                Assert.IsNull(removedCard.Place);
            }

            Assert.IsNull(_container.Pop());
        }

        [TestMethod()]
        public void CleanInactiveCardsTest()
        {
            CollectionChangedRecorder recorder = new(_container);

            recorder.Start();
            _container.CleanInactiveCards();
            recorder.Stop();
            Assert.AreEqual(0, recorder.OldItems.Count());

            foreach (Minion m in _container)
            {
                m.Hp.Set(0);
            }

            recorder.Start();
            Assert.AreEqual(0, recorder.OldItems.Count());
            _container.CleanInactiveCards();
            recorder.Stop();

            Assert.AreEqual(_initCards.Count, recorder.Recorded.Count);
            Assert.AreEqual(_initCards.Count, recorder.OldItems.Count());
        }

        [TestMethod()]
        public void RemoveIfTest()
        {
            CollectionChangedRecorder recorder = new(_container);

            recorder.Start();
            _container.RemoveIf(_initCards.Contains);
            recorder.Stop();

            Assert.AreEqual(_initCards.Count, recorder.OldItems.Count());
            Assert.AreEqual(0, _container.Count);

            List<object?> oldItems = recorder.OldItems.ToList();
            for (int i = 0; i < oldItems.Count; i++)
            {
                ICard? card = (ICard)oldItems[i]!;
                Assert.IsNotNull(card);
                Assert.IsNull(card.Place);
                Assert.AreEqual(_initCards[i], card);
            }

            recorder.Start();
            _container.RemoveIf(_initCards.Contains);
            recorder.Stop();
            Assert.AreEqual(0, recorder.OldItems.Count());
        }

        [TestMethod()]
        public void LeftTests()
        {
            for (int i = 0; i <= _container.Count; i++)
            {
                ICard? expectedCard = i == 0 ? null : _initCards[i - 1];
                Assert.AreEqual(expectedCard, _container.Left(i));
            }

            for (int i = -10; i <= 10; i++)
            {
                if (i >= 0 && i <= _container.Count) { continue; }
                Assert.IsNull(_container.Left(i));
            }
        }

        [TestMethod()]
        public void RightTests()
        {
            for (int i = _container.Count - 1; i >= -1; i--)
            {
                ICard? expectedCard = i == _container.Count - 1 ? null : _initCards[i + 1];
                Assert.AreEqual(expectedCard, _container.Right(i));
            }

            for (int i = -10; i <= 10; i++)
            {
                if (i >= -1 && i <= _container.Count - 1) { continue; }
                Assert.IsNull(_container.Right(i));
            }
        }

        [TestMethod()]
        public void MoveToContainerTests()
        {
            DoMoveToContainerTests(false);
        }

        [TestMethod()]
        public void MoveToContainerTests_WithTransformTo()
        {
            DoMoveToContainerTests(true);
        }

        public void DoMoveToContainerTests(bool shouldTransform)
        {
            IContainer _containerTo = _bf.Enemy.Field;

            Assert.AreEqual(_initCards.Count, _container.Count);
            int initialCount = _container.Count;
            for (int i = 0; i < initialCount; i++)
            {
                ICard expectedCard = _container[0];

                if (shouldTransform)
                {
                    expectedCard = new ChillwindYeti();
                    _container.MoveToContainer(0, _containerTo, false, toIndex: i, transformTo: expectedCard)();
                }
                else
                {
                    _container.MoveToContainer(0, _containerTo, false, toIndex: i)();
                }


                Assert.AreEqual(expectedCard, _containerTo[i]);
                ContainerTestHelpers.AssertCardsHaveValidPlaces(_container);
                ContainerTestHelpers.AssertCardsHaveValidPlaces(_containerTo);
            }
            Assert.AreEqual(0, _container.Count);
            Assert.AreEqual(_initCards.Count, _containerTo.Count);
        }
    }
}
