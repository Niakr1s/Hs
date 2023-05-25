using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLib.Types.Effects
{
    public class TransformEffect : IEffect
    {
        public Action UseEffect(Battlefield bf, ICard owner, ICard? target)
        {
            if (target is null) { throw new ValidationException("target is null"); }
            return () => bf[owner.PlaceInContainer!][owner.PlaceInContainer!.Index] = target.Clone();
        }
    }
}
