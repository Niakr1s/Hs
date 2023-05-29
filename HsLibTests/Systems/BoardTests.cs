using HsLib.KnownCards.Minions;
using HsLib.KnownCards.Weapons;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Containers;
using HsLib.Types.Places;

namespace HsLibTests.Systems
{
    [TestClass()]
    public class BoardTests
    {
        private Board _board = null!;

        [TestInitialize()]
        public void BoardTestsInitialize()
        {
            _board = TestBoard.New();
        }

        [TestMethod()]
        public void CardsTest_CountValid()
        {
            int expectedCount = 4; // 2x hero + ability
            Assert.AreEqual(expectedCount, _board.Cards.Count());

            _board.Player.Weapon = new FieryWarAxe();
            _board.Enemy.Weapon = new FieryWarAxe();

            expectedCount += 2;
            Assert.AreEqual(expectedCount, _board.Cards.Count());

            var doTest = (IContainer container) =>
            {
                container.Add(new ChillwindYeti());
                Assert.AreEqual(++expectedCount, _board.Cards.Count());
            };

            Pid[] pids = Pids.All();
            for (int i = 0; i < 2; i++)
            {
                doTest(_board[pids[i % 2]].Deck);
                doTest(_board[pids[i % 2]].Hand);
                doTest(_board[pids[i % 2]].Field);
            }
        }

        [TestMethod()]
        public void CardsTest_ShouldBeInChronologicalOrder()
        {
            List<Minion> cards = new();
            for (int i = 0; i < 6; i++) { cards.Add(new ChillwindYeti()); }

            Pid[] pids = Pids.All();
            for (int i = 0; i < 2; i++) { _board[pids[i % 2]].Deck.Add(cards[i]); }
            for (int i = 2; i < 4; i++) { _board[pids[i % 2]].Hand.Add(cards[i]); }
            for (int i = 4; i < 6; i++) { _board[pids[i % 2]].Field.Add(cards[i]); }

            List<ICard> boardCards = _board.Cards.TakeLast(6).ToList();
            for (int i = 0; i < 6; i++) { Assert.AreEqual(cards[i], boardCards[i]); }

            Minion newCard = new ChillwindYeti();
            _board[Pid.P2].Hand.Add(newCard);

            boardCards = _board.Cards.TakeLast(7).ToList();
            for (int i = 0; i < 6; i++) { Assert.AreEqual(cards[i], boardCards[i]); }
            Assert.AreEqual(newCard, boardCards[^1]);
        }

        [TestMethod()]
        public void WeaponAttackTest_ShouldWorkOnEnemyHero()
        {
            Weapon weapon = new FieryWarAxe();
            _board.Player.Weapon = weapon;

            int playerStartHp = _board.Player.Hero.Hp;
            int enemyStartHp = _board.Enemy.Hero.Hp;

            Assert.AreEqual(true, _board.WeaponAttack(Loc.Hero));
            Assert.AreEqual(playerStartHp, _board.Player.Hero.Hp);
            Assert.AreEqual(enemyStartHp - weapon.Atk, _board.Enemy.Hero.Hp);
        }

        [TestMethod()]
        public void WeaponAttackTest_ShouldWorkOnEnemyFieldMinion()
        {
            Weapon weapon = new FieryWarAxe();
            _board.Player.Weapon = weapon;

            Minion minion = new ChillwindYeti();
            _board.Enemy.Field.Add(minion);

            int playerStartHp = _board.Player.Hero.Hp;
            int minionHp = minion.Hp;
            Assert.AreEqual(true, _board.WeaponAttack(Loc.Field, 0));
            Assert.AreEqual(playerStartHp - minion.Atk, _board.Player.Hero.Hp);
            Assert.AreEqual(minionHp - weapon.Atk, minion.Hp);
        }

        [TestMethod()]
        public void WeaponAttackTest_ShouldNotWorkOnEnemyMinionOutsideBoard()
        {
            Weapon weapon = new FieryWarAxe();
            _board.Player.Weapon = weapon;

            Minion minion = new ChillwindYeti();

            int playerStartHp = _board.Player.Hero.Hp;
            int minionHp = minion.Hp;
            Assert.AreEqual(false, _board.WeaponAttack(Loc.Field, 0));
            Assert.AreEqual(playerStartHp, _board.Player.Hero.Hp);
            Assert.AreEqual(minionHp, minion.Hp);
        }

        [TestMethod()]
        public void WeaponAttackTest_FailCases()
        {
            Weapon weapon = new FieryWarAxe();
            _board.Player.Weapon = weapon;

            Minion minion = new ChillwindYeti();

            int playerStartHp = _board.Player.Hero.Hp;
            int minionHp = minion.Hp;

            void testSameHp()
            {
                Assert.AreEqual(playerStartHp, _board.Player.Hero.Hp);
                Assert.AreEqual(minionHp, minion.Hp);
            }

            _board.Enemy.Deck.Add(minion);
            Assert.AreEqual(1, _board.Enemy.Deck.Count);
            Assert.AreEqual(false, _board.WeaponAttack(Loc.Deck, 0));
            testSameHp();

            _board.Enemy.Deck.TakeNextCard();
            Assert.AreEqual(1, _board.Enemy.Hand.Count);
            Assert.AreEqual(false, _board.WeaponAttack(Loc.Hand, 0));
            testSameHp();

            _board[minion.Place].Remove(minion);
            _board.Enemy.Field.Add(minion);
            Assert.AreEqual(1, _board.Enemy.Field.Count);
            Assert.AreEqual(true, _board.WeaponAttack(Loc.Field, 0));
        }

        [TestMethod()]
        public void PlayFromHand_Works()
        {
            _board.Player.Hand.Add(new ChillwindYeti());
            Assert.AreEqual(true, _board.PlayFromHand(0, 0, null));
            Assert.AreEqual(0, _board.Player.Hand.Count);
            Assert.AreEqual(1, _board.Player.Field.Count);
        }
    }
}