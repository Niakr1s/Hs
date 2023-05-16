using HsLib.Cards.Minions;

using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Containers;
using HsLib.Types.Stats;
using HsLibTests.Helpers;

namespace HsLibTests.Types.Containers
{
    [TestClass()]
    public class HandTests
    {
        private Battlefield _bf = null!;

        /// <summary>
        /// Inits with minion x 3.
        /// </summary>
        private Hand _hand = null!;
        private readonly List<Minion> _initCards =
            new() { new ChillwindYeti(), new ChillwindYeti(), new ChillwindYeti() };

        /// <summary>
        /// Creates container and fills it with <see cref="_initCards"/>.
        /// </summary>
        [TestInitialize()]
        public void ContainerTestInitialize()
        {
            _bf = TestBattlefield.New();
            _hand = _bf.Player.Hand;

            foreach (Minion card in _initCards)
            {
                _hand.Add(card);
            }
        }

        [TestMethod()]
        public void Limit()
        {
            Assert.AreEqual(10, _hand.Limit);
        }

        [TestMethod()]
        public void PlayFromHandTest_Works()
        {
            Field field = _bf[_hand.Place.Pid].Field;

            int handCount = _hand.Count;
            Assert.AreNotEqual(0, handCount);

            for (int i = 0; i < handCount; i++)
            {
                Minion cardToPlay = (Minion)_hand[0];
                cardToPlay.Mp.Set(0);
                _bf.Turn.Skip(_hand.Place.Pid);

                _hand.PlayFromHand(0, fieldIndex: 0)();
                Assert.AreEqual(cardToPlay, field[0]);
            }
        }

        [TestMethod()]
        public void PlayFromHandTest_WorksWithNullIndex()
        {
            Field field = _bf[_hand.Place.Pid].Field;

            int handCount = _hand.Count;
            Assert.AreNotEqual(0, handCount);

            for (int i = 0; i < handCount; i++)
            {
                Minion cardToPlay = (Minion)_hand[0];
                cardToPlay.Mp.Set(0);
                _bf.Turn.Skip(_hand.Place.Pid);

                _hand.PlayFromHand(0, fieldIndex: null)();
                Assert.AreEqual(cardToPlay, field[^1]);
            }
        }

        [TestMethod()]
        public void PlayFromHandTest_NonEffectMinion_NullEffectTarget()
        {
            Field field = _bf[_hand.Place.Pid].Field;

            int handCount = _hand.Count;
            Assert.AreNotEqual(0, handCount);

            _hand.Clear();

            Minion target = new ChillwindYeti();
            int targetStartAtk = target.Atk;
            field.Add(target);

            Minion minion = new ChillwindYeti();
            minion.Mp.Set(0);
            _hand.Add(minion);

            _hand.PlayFromHand(0, effectTarget: null)();
            Assert.AreEqual(targetStartAtk, target.Atk);
        }


        [TestMethod()]
        public void PlayFromHandTest_NonEffectMinion_NonNullEffectTarget()
        {
            Field field = _bf[_hand.Place.Pid].Field;

            int handCount = _hand.Count;
            Assert.AreNotEqual(0, handCount);

            _hand.Clear();

            Minion target = new ChillwindYeti();
            int targetStartAtk = target.Atk;
            field.Add(target);

            Minion minion = new ChillwindYeti();
            minion.Mp.Set(0);
            _hand.Add(minion);

            Assert.ThrowsException<ValidationException>(() => _hand.PlayFromHand(0, effectTarget: target));
            Assert.AreEqual(targetStartAtk, target.Atk);
        }


        [TestMethod()]
        public void PlayFromHandTest_EffectMinion_NullEffectTarget()
        {
            Field field = _bf[_hand.Place.Pid].Field;

            int handCount = _hand.Count;
            Assert.AreNotEqual(0, handCount);

            _hand.Clear();

            Minion target = new ChillwindYeti();
            int targetStartAtk = target.Atk;
            field.Add(target);

            Minion minion = new AbusiveSergeant();
            minion.Mp.Set(0);
            _hand.Add(minion);

            Assert.ThrowsException<ValidationException>(() => _hand.PlayFromHand(0, effectTarget: null));
            Assert.AreEqual(targetStartAtk, target.Atk);
        }


        [TestMethod()]
        public void PlayFromHandTest_EffectMinion_NonNullEffectTarget()
        {
            Field field = _bf[_hand.Place.Pid].Field;

            int handCount = _hand.Count;
            Assert.AreNotEqual(0, handCount);

            _hand.Clear();

            Minion target = new ChillwindYeti();
            int targetStartAtk = target.Atk;
            field.Add(target);

            Minion minion = new AbusiveSergeant();
            minion.Mp.Set(0);
            _hand.Add(minion);

            _hand.PlayFromHand(0, effectTarget: target)();
            Assert.AreEqual(targetStartAtk + 2, target.Atk);
        }

        [TestMethod()]
        public void PlayFromHandTest_FailsOnWrongIndex()
        {
            Field field = _bf[_hand.Place.Pid].Field;

            int handCount = _hand.Count;
            for (int i = -20; i < 20; i++)
            {
                if (i >= 0 && i < handCount) { continue; }
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => _hand.PlayFromHand(i, 0));
            }
        }

        [TestMethod()]
        public void PlayFromHandTest_WorksOnEnoughMp()
        {
            Minion cardToPlay = (Minion)_hand[0];
            Assert.AreNotEqual(0, cardToPlay.Mp);

            Mp playerMp = _bf[_hand.Place.Pid].Mp;
            int initialMp = playerMp;

            _hand.PlayFromHand(0, fieldIndex: 0)();
            Assert.AreEqual(initialMp - cardToPlay.Mp, playerMp);
        }

        [TestMethod()]
        public void PlayFromHandTest_FailsOnNotEnoughMp()
        {
            Minion cardToPlay = (Minion)_hand[0];
            Assert.AreNotEqual(0, cardToPlay.Mp);

            Mp playerMp = _bf[_hand.Place.Pid].Mp;
            playerMp.Set(cardToPlay.Mp - 1);
            int initialMp = playerMp;

            Assert.ThrowsException<ValidationException>(() => _hand.PlayFromHand(0, fieldIndex: 0)());
            Assert.AreEqual(initialMp, playerMp);
        }

    }
}