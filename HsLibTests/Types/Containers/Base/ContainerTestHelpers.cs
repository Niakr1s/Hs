using HsLib.Interfaces;
using HsLib.Types.Containers.Base;

namespace HsLibTests.Types.Containers.Base
{
    internal static class ContainerTestHelpers
    {
        internal static void AssertCardsHaveValidPlaces(IContainer container)
        {
            Assert.IsNotNull(container.Place);
            for (int i = 0; i < container.Count; i++)
            {
                ICard card = (ICard)container[i]!;
                Assert.IsNotNull(card.PlaceInContainer);
                Assert.AreEqual(container.Place, card.PlaceInContainer);
                Assert.AreEqual(i, card.PlaceInContainer.Index);
            }
        }

        internal static void AssertCardIsSet<TCard>(SingleContainer<TCard> container, TCard card)
            where TCard : ICard
        {
            AssertCardsHaveValidPlaces(container);

            Assert.AreEqual(card, container.Card);

            Assert.IsNotNull(card.PlaceInContainer);
            Assert.AreEqual(container.Place, card.PlaceInContainer);
        }
    }
}
