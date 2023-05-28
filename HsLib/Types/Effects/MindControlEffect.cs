﻿using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Containers;
using HsLib.Types.Places;

namespace HsLib.Types.Effects
{
    public class MindControlEffect : IEffect
    {
        public Action UseEffect(Battlefield bf, ICard owner, ICard? target)
        {
            if (target is Minion m)
            {

                Field enemyField = bf[m.Place.Pid].Field;
                Field playerField = bf[m.Place.Pid.He()].Field;

                if (!enemyField.Contains(m)) { throw new ValidationException("enemy field doesn't contain target"); }
                if (playerField.IsFull) { throw new ValidationException("can't insert to full field"); }

                return () =>
                {
                    enemyField.Remove(m);
                    playerField.Add(m);
                };
            }
            else
            {
                throw new ValidationException("target is not Minion");
            }
        }
    }
}