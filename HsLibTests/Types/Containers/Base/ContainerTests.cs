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
        public void AddTest()
        {
            int startCount = _container.Count;
            _container.Add(new ChillwindYeti());
            AssertCardsHaveValidPlaces();
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
                AssertCardsHaveValidPlaces();

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
                AssertCardsHaveValidPlaces();
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
                AssertCardsHaveValidPlaces(_container);
                AssertCardsHaveValidPlaces(_containerTo);
            }
            Assert.AreEqual(0, _container.Count);
            Assert.AreEqual(_initCards.Count, _containerTo.Count);
        }

        #region helpers

        private void AssertCardsHaveValidPlaces()
        {
            AssertCardsHaveValidPlaces(_container);
        }

        private static void AssertCardsHaveValidPlaces(IContainer container)
        {
            Assert.IsNotNull(container.Place);
            for (int i = 0; i < container.Count; i++)
            {
                ICard card = (ICard)container[i]!;
                Assert.IsNotNull(card.Place);
                Assert.AreEqual(container.Place, card.Place);
                Assert.AreEqual(i, card.Place.Index);
            }
        }

        #endregion
    }
}

/*
using HsLib.Cards.Minions;
using HsLib.Extensions;
using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Containers.Base;
using HsLibTests.Helpers;

namespace HsLib.Types.Containers
{
    [TestClass()]
    public class ContainerTests
    {
        private Battlefield _bf = null!;
        private IContainer _container = null!;
        private List<ICard> _initCards =
            new() { new ChillwindYeti(), new ChillwindYeti(), new ChillwindYeti() };

        /// <summary>
        /// Creates container and fills it with <see cref="_initCards"/>.
        /// </summary>
        [TestInitialize()]
        public void ContainerTestInitialize()
        {
            _bf = TestBattlefield.New();
            _container = _bf.Player.Field;

            foreach (ICard card in _initCards)
            {
                _container.Add(card);
            }
        }

        [TestMethod()]
        public void IndexerTests()
        {
            for (int i = 0; i < _initCards.Count; i++)
            {
                Assert.AreEqual(_initCards[i], _container[i]);
            }

            for (int i = -10; i < 10; i++)
            {
                if (i < 0 || i >= _initCards.Count)
                {
                    Assert.ThrowsException<ArgumentOutOfRangeException>(() => _container[i]);
                }
            }
        }

        [TestMethod()]
        public void CardsTests()
        {
            foreach ((ICard card, int i) in _container.Cards.WithIndex())
            {
                Assert.AreEqual(_initCards[i], card);
            }
            CardsHaveValidPlaces();
        }

        private void CardsHaveValidPlaces()
        {
            CardsHaveValidPlaces(_container);
        }

        private void CardsHaveValidPlaces(IContainer container)
        {
            Assert.IsNotNull(container.Place);
            foreach ((ICard card, int i) in container.Cards.WithIndex())
            {
                Assert.IsNotNull(card.Place);
                Assert.AreEqual(container.Place, card.Place);
                Assert.AreEqual(i, card.Place.Index);
            }
        }

        [TestMethod()]
        public void CountTests()
        {
            Assert.AreEqual(_initCards.Count, _container.Count);
        }

        [TestMethod()]
        public void AddTests()
        {
            int startCount = _container.Count;
            _container.Add(new ChillwindYeti());
            CardsHaveValidPlaces();
            Assert.AreEqual(startCount + 1, _container.Count);

        }

        [TestMethod()]
        public void InsertTests()
        {
            int startCount = _container.Count;
            for (int i = _container.Count; i >= 0; i--)
            {
                Minion m = new ChillwindYeti();
                _container.Insert(i, m);
                CardsHaveValidPlaces();

                Assert.AreEqual(_container.Place, m.Place!);
                Assert.AreEqual(++startCount, _container.Count);
            }
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _container.Insert(_container.Count + 1, new ChillwindYeti()));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _container.Insert(-1, new ChillwindYeti()));
        }


        [TestMethod()]
        public void ReplaceTests()
        {
            for (int i = 0; i < _initCards.Count; i++)
            {
                Minion m = new ChillwindYeti();
                RemovedCard removedCard = _container.Replace(i, m);
                CardsHaveValidPlaces();
                Assert.AreEqual(m, _container[i]);
                Assert.AreEqual(_initCards[i], removedCard.Card);
                Assert.AreEqual(m.Place!, removedCard.Place);
            }
        }

        [TestMethod()]
        public void RemoveAtTests()
        {
            for (int i = _initCards.Count - 1; i >= 0; i--)
            {
                Place oldPlace = _container[i].Place!;
                RemovedCard removedCard = _container.RemoveAt(i);
                CardsHaveValidPlaces();
                Assert.IsNull(removedCard.Card.Place);
                Assert.AreEqual(_initCards[i], removedCard.Card);
                Assert.AreEqual(oldPlace, removedCard.Place);
            }
            Assert.AreEqual(0, _container.Count);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _container.RemoveAt(-1));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _container.RemoveAt(0));
        }

        [TestMethod()]
        public void PopTests()
        {
            for (int i = _initCards.Count - 1; i >= 0; i--)
            {
                RemovedCard? removedCard = _container.Pop();
                CardsHaveValidPlaces();
                Assert.IsNotNull(removedCard);
                Assert.IsNull(removedCard.Card.Place);
                Assert.AreEqual(_container.Place, removedCard.Place);
            }

            Assert.IsNull(_container.Pop());
        }

        [TestMethod()]
        public void CanBeInsertedAtTests()
        {
            for (int i = -10; i < 10; i++)
            {
                Assert.AreEqual(i >= 0 && i <= _initCards.Count, _container.CanBeInsertedAt(i));
            }
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
        public void RemoveIfTests()
        {
            List<RemovedCard> removed = _container.RemoveIf(_initCards.Contains).ToList();
            Assert.AreEqual(_initCards.Count, removed.Count);
            Assert.AreEqual(0, _container.Count);
            foreach ((RemovedCard card, int i) in removed.WithIndex())
            {
                Assert.AreEqual(_container.Place, card.Place);
                Assert.AreEqual(_initCards[i], card.Card);
            }

            Assert.AreEqual(0, _container.RemoveIf(_initCards.Contains).Count());
        }

        [TestMethod()]
        public void RemoveInactiveCardsTests()
        {
            Assert.AreEqual(0, _container.RemoveInactiveCards().Count());

            foreach (var card in _container.Cards)
            {
                Minion m = (Minion)card;
                m.Hp.Set(0);
            }

            Assert.AreEqual(_initCards.Count, _container.RemoveInactiveCards().Count());
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
                CardsHaveValidPlaces(_container);
                CardsHaveValidPlaces(_containerTo);
            }
            Assert.AreEqual(0, _container.Count);
            Assert.AreEqual(_initCards.Count, _containerTo.Count);
        }
    }
}
*/