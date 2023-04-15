﻿using Models.Cards.KnownCards.Minions;
using Models.Common.Place;
using Models.Containers;

namespace Models.Cards.KnownCards.Tests
{
    [TestClass()]
    public class AbusiveSergeantTests
    {
        [TestMethod()]
        public void AbusiveSergeantTest()
        {
            Battlefield bf = new Battlefield(HeroId.Jaina, HeroId.Rexxar);
            bf.Turn.Next();

            Minion yeti = new ChillwindYeti();
            bf[Pid.P1].Field.Add(yeti);
            Assert.AreEqual(4, yeti.Atk.Value);

            Minion abusiveSergeant = new AbusiveSergeant();
            bf[Pid.P1].Hand.Add(abusiveSergeant);
            Assert.AreEqual(4, yeti.Atk.Value);

            abusiveSergeant.Battlecry?.UseEffect(bf, abusiveSergeant, yeti);
            Assert.AreEqual(6, yeti.Atk.Value);

            bf.Turn.Next();
            Assert.AreEqual(4, yeti.Atk.Value);
        }
    }
}