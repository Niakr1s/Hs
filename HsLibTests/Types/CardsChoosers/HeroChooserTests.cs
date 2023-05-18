using HsLib.KnownCards.Abilities;
using HsLib.KnownCards.Heroes;
using HsLib.KnownCards.Minions;
using HsLib.KnownCards.Weapons;
using HsLib.Types;
using HsLib.Types.Cards;
using HsLib.Types.CardsChoosers;
using HsLib.Types.Places;

namespace HsLibTests.Types.CardsChoosers
{
    [TestClass()]
    public class HeroChooserTests
    {
        [TestMethod()]
        public void HeroChooserTest()
        {
            var chooser = new HeroChooser();

            List<ICard> Cards = new() {
                // hero with wrong Pid at first place
                new JainaProudmoore() { PlaceInContainer = new Place(Pid.P2, Loc.Hero).InContainer(0, 0) },

                // correct cards
                new GarroshHellscream() { PlaceInContainer = new Place(Pid.P1, Loc.Hero).InContainer(0, 0) },
                new JainaProudmoore() { PlaceInContainer = new Place(Pid.P1, Loc.Hero).InContainer(0, 0) },

                // heroes in other locations
                new GarroshHellscream() { PlaceInContainer = new Place(Pid.P1, Loc.Deck).InContainer(0, 0) },

                // non-heros
                new ChillwindYeti() {  PlaceInContainer = new Place(Pid.P1, Loc.Hero).InContainer(0, 0)  },
                new Fireblast() {  PlaceInContainer = new Place(Pid.P1, Loc.Hero).InContainer(0, 0)  },
                new FieryWarAxe() {  PlaceInContainer = new Place(Pid.P1, Loc.Hero).InContainer(0, 0)  },
            };

            Assert.AreEqual(2, chooser.ChooseCards(Pid.P1, Cards).Count());
        }
    }
}