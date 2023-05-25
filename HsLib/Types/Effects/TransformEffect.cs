using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLib.Types.Effects
{
    public class TransformEffect : IEffect
    {
        public int Damage { get; set; }

        public Action UseEffect(Battlefield bf, ICard owner, ICard target)
        {
            if (target is Minion m)
            {
                return () => bf[owner.PlaceInContainer!][owner.PlaceInContainer!.Index] = m.Clone();
            }
            else
            {
                throw new ValidationException("can't transform");
            }
        }
    }
}
