using HsLib.KnownCards.Abilities;
using HsLib.KnownCards.Minions;
using HsLib.KnownCards.Spells;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Effects;
using HsLib.Types.Places;

namespace HsLibTests.KnownCards.Minions
{
    [TestClass()]
    public class ProphetVelenTests
    {
        [TestMethod()]
        public void ProphetVelenTest_Ability()
        {
            Board board = TestBoard.New();
            board.Player.Ability = new Fireblast();

            Minion yeti = new ChillwindYeti();
            int expectedHp = yeti.Hp;

            board.Enemy.Field.Add(yeti);
            Assert.AreEqual(expectedHp, yeti.Hp);
            Assert.AreEqual(1, ((IDamageEffect)board.Player.Ability.AbilityEffect.Effect).DamageAmount);

            Assert.AreEqual(true, board.UseAbility(Pid.P2, Loc.Field, 0));
            expectedHp--;
            Assert.AreEqual(expectedHp, yeti.Hp);

            board.Turn.Skip(board.Player.Pid);
            board.Player.Field.Add(new ProphetVelen());

            Assert.AreEqual(true, board.UseAbility(Pid.P2, Loc.Field, 0));
            expectedHp -= 2;
            Assert.AreEqual(expectedHp, yeti.Hp);
        }

        [TestMethod()]
        public void ProphetVelenTest_Spell()
        {
            Board board = TestBoard.New();

            Spell holySmite = new HolySmite();
            board.Player.Hand.Add(holySmite);

            Minion yeti = new ChillwindYeti();
            int expectedHp = yeti.Hp;

            board.Enemy.Field.Add(yeti);
            board.Player.Field.Add(new ProphetVelen());
            Assert.AreEqual(expectedHp, yeti.Hp);

            Assert.AreEqual(true, board.PlayFromHand(0, effectTarget: yeti));
            expectedHp -= 4;
            Assert.AreEqual(expectedHp, yeti.Hp);
        }
    }
}