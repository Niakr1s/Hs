using HsLib.Cards.Abilities;
using HsLib.Cards.Heroes;
using HsLib.Cards.Minions;
using HsLib.Cards.Weapons;
using HsLib.Interfaces;
using HsLib.Types;
using HsLib.Types.CardsChoosers;

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