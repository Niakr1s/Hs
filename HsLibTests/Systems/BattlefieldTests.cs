using HsLib.Cards.Minions;
using HsLib.Cards.Weapons;
using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types;
using HsLib.Types.Cards;
using HsLib.Types.Containers.Base;
using HsLibTests.Helpers;

namespace HsLibTests.Systems
{
    [TestClass()]
    public class BattlefieldTests
    {
        private Battlefield _bf = null!;

        [TestInitialize()]
        public void BattlefieldTestsInitialize()
        {
            _bf = TestBattlefield.New();
        }

        [TestMethod()]
        public void CardsTest_CountValid()
        {
            int expectedCount = 6; // 2x hero + weapon + ability
            Assert.AreEqual(expectedCount, _bf.Cards.Count());

            var doTest = (IContainer container) =>
            {
                container.Add(new ChillwindYeti());
                Assert.AreEqual(++expectedCount, _bf.Cards.Count());
            };

            Pid[] pids = Pids.All();
            for (int i = 0; i < 2; i++)
            {
                doTest(_bf[pids[i % 2]].Deck);
                doTest(_bf[pids[i % 2]].Hand);
                doTest(_bf[pids[i % 2]].Field);
            }
        }

        [TestMethod()]
        public void CardsTest_ShouldBeInChronologicalOrder()
        {
            List<ICard> cards = new();
            for (int i = 0; i < 6; i++) { cards.Add(new ChillwindYeti()); }

            Pid[] pids = Pids.All();
            for (int i = 0; i < 2; i++) { _bf[pids[i % 2]].Deck.Add(cards[i]); }
            for (int i = 2; i < 4; i++) { _bf[pids[i % 2]].Hand.Add(cards[i]); }
            for (int i = 4; i < 6; i++) { _bf[pids[i % 2]].Field.Add(cards[i]); }

            List<ICard> bfCards = _bf.Cards.TakeLast(6).ToList();
            for (int i = 0; i < 6; i++) { Assert.AreEqual(cards[i], bfCards[i]); }

            ICard newCard = new ChillwindYeti();
            _bf[Pid.P2].Hand.Add(newCard);

            bfCards = _bf.Cards.TakeLast(7).ToList();
            for (int i = 0; i < 6; i++) { Assert.AreEqual(cards[i], bfCards[i]); }
            Assert.AreEqual(newCard, bfCards[^1]);
        }

        [TestMethod()]
        public void WeaponAttackTest_ShouldWorkOnEnemyHero()
        {
            Weapon weapon = new FieryWarAxe();
            _bf.Player.Weapon.Set(weapon);

            int playerStartHp = _bf.Player.Hero.Card.Hp;
            int enemyStartHp = _bf.Enemy.Hero.Card.Hp;

            Assert.AreEqual(true, _bf.WeaponAttack(Loc.Hero));
            Assert.AreEqual(playerStartHp, _bf.Player.Hero.Card.Hp);
            Assert.AreEqual(enemyStartHp - weapon.Atk, _bf.Enemy.Hero.Card.Hp);
        }

        [TestMethod()]
        public void WeaponAttackTest_ShouldWorkOnEnemyFieldMinion()
        {
            Weapon weapon = new FieryWarAxe();
            _bf.Player.Weapon.Set(weapon);

            Minion minion = new ChillwindYeti();
            _bf.Enemy.Field.Add(minion);

            int playerStartHp = _bf.Player.Hero.Card.Hp;
            int minionHp = minion.Hp;
            Assert.AreEqual(true, _bf.WeaponAttack(Loc.Field, 0));
            Assert.AreEqual(playerStartHp - minion.Atk, _bf.Player.Hero.Card.Hp);
            Assert.AreEqual(minionHp - weapon.Atk, minion.Hp);
        }

        [TestMethod()]
        public void WeaponAttackTest_ShouldNotWorkOnEnemyMinionOutsideBattleField()
        {
            Weapon weapon = new FieryWarAxe();
            _bf.Player.Weapon.Set(weapon);

            Minion minion = new ChillwindYeti();

            int playerStartHp = _bf.Player.Hero.Card.Hp;
            int minionHp = minion.Hp;
            Assert.AreEqual(false, _bf.WeaponAttack(Loc.Field, 0));
            Assert.AreEqual(playerStartHp, _bf.Player.Hero.Card.Hp);
            Assert.AreEqual(minionHp, minion.Hp);
        }

        [TestMethod()]
        public void WeaponAttackTest_FailCases()
        {
            Weapon weapon = new FieryWarAxe();
            _bf.Player.Weapon.Set(weapon);

            Minion minion = new ChillwindYeti();

            int playerStartHp = _bf.Player.Hero.Card.Hp;
            int minionHp = minion.Hp;

            void testSameHp()
            {
                Assert.AreEqual(playerStartHp, _bf.Player.Hero.Card.Hp);
                Assert.AreEqual(minionHp, minion.Hp);
            }

            _bf.Enemy.Deck.Add(minion);
            Assert.AreEqual(1, _bf.Enemy.Deck.Count);
            Assert.AreEqual(false, _bf.WeaponAttack(Loc.Deck, 0));
            testSameHp();

            _bf.Enemy.Deck.TakeNextCard();
            Assert.AreEqual(1, _bf.Enemy.Hand.Count);
            Assert.AreEqual(false, _bf.WeaponAttack(Loc.Hand, 0));
            testSameHp();

            _bf.Enemy.Hand.PlayFromHand(0)();
            Assert.AreEqual(1, _bf.Enemy.Field.Count);
            Assert.AreEqual(true, _bf.WeaponAttack(Loc.Field, 0));
        }
    }
}