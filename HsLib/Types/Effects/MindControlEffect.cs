using HsLib.Exceptions;
using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Containers;

namespace HsLib.Types.Effects
{
    public class MindControlEffect : IEffect
    {
        public Action UseEffect(Battlefield bf, ICard target)
        {
            Minion m = (Minion)target;
            Field enemyField = bf[m.Place!.Pid].Field;
            Field playerField = bf[m.Place!.Pid.He()].Field;

            if (!enemyField.Contains(m)) { throw new ValidationException("enemy field doesn't contain target"); }
            if (!playerField.CanBeInsertedAt(playerField.Count)) { throw new ValidationException("can't insert to player field"); }

            return () =>
            {
                enemyField.Remove(m);
                playerField.Add(m);
            };
        }
    }
}