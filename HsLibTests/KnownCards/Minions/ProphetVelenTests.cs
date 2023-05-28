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
            Battlefield bf = TestBattlefield.New();
            bf.Player.Ability = new Fireblast();

            Minion yeti = new ChillwindYeti();
            int expectedHp = yeti.Hp;

            bf.Enemy.Field.Add(yeti);
            Assert.AreEqual(expectedHp, yeti.Hp);
            Assert.AreEqual(1, ((IDamageEffect)bf.Player.Ability.AbilityEffect.Effect).DamageAmount);

            Assert.AreEqual(true, bf.UseAbility(Pid.P2, Loc.Field, 0));
            expectedHp--;
            Assert.AreEqual(expectedHp, yeti.Hp);

            bf.Turn.Skip(bf.Player.Pid);
            bf.Player.Field.Add(new ProphetVelen());

            Assert.AreEqual(true, bf.UseAbility(Pid.P2, Loc.Field, 0));
            expectedHp -= 2;
            Assert.AreEqual(expectedHp, yeti.Hp);
        }

        [TestMethod()]
        public void ProphetVelenTest_Spell()
        {
            Battlefield bf = TestBattlefield.New();

            Spell holySmite = new HolySmite();
            bf.Player.Hand.Add(holySmite);

            Minion yeti = new ChillwindYeti();
            int expectedHp = yeti.Hp;

            bf.Enemy.Field.Add(yeti);
            bf.Player.Field.Add(new ProphetVelen());
            Assert.AreEqual(expectedHp, yeti.Hp);

            Assert.AreEqual(true, bf.PlayFromHand(0, effectTarget: yeti));
            expectedHp -= 4;
            Assert.AreEqual(expectedHp, yeti.Hp);
        }
    }
}