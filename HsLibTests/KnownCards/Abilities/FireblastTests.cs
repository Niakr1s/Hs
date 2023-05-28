using HsLib.KnownCards.Abilities;
using HsLib.KnownCards.Minions;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Places;



namespace HsLibTests.KnownCards.Abilities
{
    [TestClass()]
    public class FireblastTests
    {

        [TestMethod()]
        public void FireblastTest()
        {
            Battlefield bf = TestBattlefield.New(p1: CardId.JainaProudmoore);
            Ability fireblast = bf.Player.Ability;
            Assert.IsInstanceOfType(fireblast, typeof(Fireblast));
            Assert.AreEqual(2, fireblast.AbilityEffect.GetPossibleTargets(bf, fireblast.PlaceInContainer!.Pid).Count());

            int startMp = bf.Player.Mp;

            Assert.AreEqual(30, bf.Player.Hero.Hp);
            fireblast.UseAbility(bf, bf.Player.Hero)();
            Assert.AreEqual(29, bf.Player.Hero.Hp);
            Assert.AreEqual(startMp - 2, bf.Player.Mp);

            Minion y1 = new ChillwindYeti();
            Minion y2 = new ChillwindYeti();

            bf[Pid.P1].Field.Add(y1);
            Assert.AreEqual(3, fireblast.AbilityEffect.GetPossibleTargets(bf, fireblast.PlaceInContainer!.Pid).Count());
            bf[Pid.P2].Field.Add(y2);
            Assert.AreEqual(4, fireblast.AbilityEffect.GetPossibleTargets(bf, fireblast.PlaceInContainer!.Pid).Count());

            Assert.AreEqual(5, y1.Hp);
            Assert.AreEqual(5, y2.Hp);

            bf.Turn.Skip(bf.Player.Pid);
            fireblast.UseAbility(bf, y1)();
            Assert.AreEqual(4, y1.Hp);

            bf.Turn.Skip(bf.Player.Pid);
            fireblast.UseAbility(bf, y2)();
            Assert.AreEqual(4, y2.Hp);
        }
    }
}