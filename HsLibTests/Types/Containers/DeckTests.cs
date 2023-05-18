using HsLib.KnownCards.Minions;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Containers;
using HsLibTests.Helpers;
using HsLibTests.Types.Containers.Base;

namespace HsLibTests.Types.Containers
{
    [TestClass()]
    public class DeckTests
    {
        private Battlefield _bf = null!;

        /// <summary>
        /// Inits with minion x 3.
        /// </summary>
        private Deck _deck = null!;
        private readonly List<Minion> _initCards =
            new() { new ChillwindYeti(), new ChillwindYeti(), new ChillwindYeti() };

        /// <summary>
        /// Creates container and fills it with <see cref="_initCards"/>.
        /// </summary>
        [TestInitialize()]
        public void ContainerTestInitialize()
        {
            _bf = TestBattlefield.New();
            _deck = _bf.Player.Deck;

            foreach (Minion card in _initCards)
            {
                _deck.Add(card);
            }
        }

        [TestMethod()]
        public void Limit()
        {
            Assert.AreEqual(null, _deck.Limit);
        }

        [TestMethod()]
        public void TakeNextCardTest()
        {
            Hand hand = _bf[_deck.Place.Pid].Hand;

            for (int i = 0; i < _initCards.Count; i++)
            {
                _deck.TakeNextCard();
                ContainerTestHelpers.AssertCardsHaveValidPlaces(_deck);
                ContainerTestHelpers.AssertCardsHaveValidPlaces(hand);
                Assert.AreEqual(_initCards.Count - i - 1, _deck.Count);
                Assert.AreEqual(i + 1, hand.Count);
            }

            _deck.TakeNextCard();
            ContainerTestHelpers.AssertCardsHaveValidPlaces(_deck);
            ContainerTestHelpers.AssertCardsHaveValidPlaces(hand);
            Assert.AreEqual(0, _deck.Count);
            Assert.AreEqual(_initCards.Count, hand.Count);

            while (!hand.IsFull) { hand.Add(new ChillwindYeti()); }
            ContainerTestHelpers.AssertCardsHaveValidPlaces(_deck);
            ContainerTestHelpers.AssertCardsHaveValidPlaces(hand);
            Assert.AreEqual(hand.Limit, hand.Count);

            Minion m = new ChillwindYeti();
            _deck.Add(m);
            _deck.TakeNextCard();
            ContainerTestHelpers.AssertCardsHaveValidPlaces(_deck);
            ContainerTestHelpers.AssertCardsHaveValidPlaces(hand);
            Assert.AreEqual(0, _deck.Count);
            Assert.AreEqual(hand.Limit, hand.Count);
            Assert.AreEqual(false, hand.Contains(m));
        }
    }
}