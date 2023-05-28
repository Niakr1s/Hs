﻿using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Containers;

namespace HsLib.Types.Effects
{
    public class TransformEffect : IEffect
    {
        public Action UseEffect(Battlefield bf, ICard owner, ICard? target)
        {
            if (target is null) { throw new ValidationException("target is null"); }

            IContainer container = bf[owner.Place];
            int index = container.IndexOf(owner);
            if (index == -1) { throw new ValidationException("no card in container"); }

            return () => container[index] = target.Clone();
        }
    }
}
