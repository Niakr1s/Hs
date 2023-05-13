using HsLib.Cards.Abilities;
using HsLib.Cards.Minions;
using HsLib.Systems;
using HsLib.Types;
using HsLib.Types.Cards;
using HsLibTests.Helpers;

namespace HsLibTests.Cards.Abilities
{
    [TestClass()]
    public class FireblastTests
    {

        [TestMethod()]
        public void FireblastTest()
        {
            Battlefield bf = TestBattlefield.New(p1: CardId.JainaProudmoore);
            Ability fireblast = bf.Player.Ability.Card;
            Assert.IsInstanceOfType(fireblast, typeof(Fireblast));
            Assert.AreEqual(true, fireblast.EffectIsSoloTarget);
            Assert.AreEqual(2, fireblast.UseEffectTargets(bf).Count());

            Assert.AreEqual(30, bf.Player.Hero.Card.Hp.Value);
            Assert.AreEqual(true, bf.UseAbility(bf.Player.Hero.Card));
            Assert.AreEqual(29, bf.Player.Hero.Card.Hp.Value);

            Minion y1 = new ChillwindYeti();
            Minion y2 = new ChillwindYeti();

            bf[Pid.P1].Field.Add(y1);
            Assert.AreEqual(3, fireblast.UseEffectTargets(bf).Count());
            bf[Pid.P2].Field.Add(y2);
            Assert.AreEqual(4, fireblast.UseEffectTargets(bf).Count());

            Assert.AreEqual(5, y1.Hp.Value);
            Assert.AreEqual(5, y2.Hp.Value);

            bf.Turn.Skip(bf.Player.Pid);
            bf.UseAbility(y1);
            Assert.AreEqual(4, y1.Hp.Value);

            bf.Turn.Skip(bf.Player.Pid);
            bf.UseAbility(y2);
            Assert.AreEqual(4, y2.Hp.Value);
        }
    }
}