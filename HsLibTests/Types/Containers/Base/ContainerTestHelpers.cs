using HsLib.Types.Cards;
using HsLib.Types.Containers;

namespace HsLibTests.Types.Containers.Base
{
    internal static class ContainerTestHelpers
    {
        internal static void AssertCardsHaveValidPlaces(IContainer container)
        {
            Assert.AreEqual(false, container.Place.IsNone());
            for (int i = 0; i < container.Count; i++)
            {
                ICard card = (ICard)container[i]!;
                Assert.AreEqual(false, card.Place.IsNone());
                Assert.AreEqual(container.Place, card.Place);
            }
        }
    }
}
