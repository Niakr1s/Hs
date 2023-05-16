﻿using HsLib.Interfaces;
using HsLib.Types.Containers.Base;

namespace HsLibTests.Types.Containers.Base
{
    internal static class AssertContainerTestHelpers
    {
        internal static void AssertCardsHaveValidPlaces(IContainer container)
        {
            Assert.IsNotNull(container.Place);
            for (int i = 0; i < container.Count; i++)
            {
                ICard card = (ICard)container[i]!;
                Assert.IsNotNull(card.Place);
                Assert.AreEqual(container.Place, card.Place);
                Assert.AreEqual(i, card.Place.Index);
            }
        }

        internal static void AssertCardIsSet<TCard>(SingleContainer<TCard> container, TCard card)
            where TCard : ICard
        {
            AssertCardsHaveValidPlaces(container);

            Assert.AreEqual(card, container.Card);

            Assert.IsNotNull(card.Place);
            Assert.AreEqual(container.Place, card.Place);
        }
    }
}
