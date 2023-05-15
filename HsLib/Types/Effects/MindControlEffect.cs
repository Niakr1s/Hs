using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Containers.Base;

namespace HsLib.Types.Effects
{
    public class MindControlEffect : IEffect
    {
        public void UseEffect(Battlefield bf, ICard target)
        {
            if (target is Minion m)
            {
                RemovedCard removedCard = bf[m.Place!.Pid].Field.Remove(m);
                bf[removedCard.Place.Pid.He()].Field.Add(m);
            }
        }
    }
}