using HsLib.Types.Cards;
using HsLib.Types.Containers;

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
    }
}
