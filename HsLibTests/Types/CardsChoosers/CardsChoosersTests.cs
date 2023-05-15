using HsLib.Cards.Abilities;
using HsLib.Cards.Heroes;
using HsLib.Cards.Minions;
using HsLib.Cards.Weapons;
using HsLib.Interfaces;

namespace HsLib.Types.CardsChoosers
{
    [TestClass()]
    public class CardsChoosersTests
    {
        [TestMethod()]
        public void HeroChooserTest()
        {
            ICardsChooser chooser = CardsChoosers.HeroChooser();

            List<ICard> Cards = new() {
                // hero with wrong Pid at first place
                new JainaProudmoore() { Place = new Place(Pid.P2, Loc.Hero).InContainer(0, 0) },

                // correct cards
                new GarroshHellscream() { Place = new Place(Pid.P1, Loc.Hero).InContainer(0, 0) },
                new JainaProudmoore() { Place = new Place(Pid.P1, Loc.Hero).InContainer(0, 0) },

                // heroes in other locations
                new GarroshHellscream() { Place = new Place(Pid.P1, Loc.Deck).InContainer(0, 0) },

                // non-heros
                new ChillwindYeti() {  Place = new Place(Pid.P1, Loc.Hero).InContainer(0, 0)  },
                new Fireblast() {  Place = new Place(Pid.P1, Loc.Hero).InContainer(0, 0)  },
                new FieryWarAxe() {  Place = new Place(Pid.P1, Loc.Hero).InContainer(0, 0)  },
            };

            Assert.AreEqual(2, chooser.ChooseCards(Pid.P1, Cards).Count());
        }
    }
}