﻿using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Containers;

namespace HsLib.Types.Choosers
{
    public class FieldAdjacentMinionsChooser : IChooser
    {
        public bool ExcludeLeft { get; init; }

        public bool ExcludeRight { get; init; }

        public IEnumerable<ICard> ChooseCards(Battlefield bf, ICard owner)
        {
            if (owner.Place.IsNone()) { yield break; }

            IContainer? container = bf[owner.Place.Pid].GetContainer(owner);
            ICard? left = container?.Left(owner);
            ICard? right = container?.Right(owner);

            foreach (var card in bf.Cards.Where(c =>
                    (!ExcludeLeft && left is not null && c == left) ||
                    (!ExcludeRight && right is not null && c == right)))
            {
                yield return card;
            }
        }
    }
}
