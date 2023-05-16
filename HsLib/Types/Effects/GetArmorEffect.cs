
using HsLib.Interfaces;
using HsLib.Interfaces.CardTraits;
using HsLib.Systems;

namespace HsLib.Types.Effects
{
    public class GetArmorEffect : IEffect
    {
        public int Armor { get; set; }

        public Action UseEffect(Battlefield bf, ICard? target)
        {
            if (target is IWithArmor a)
            {
                return () => a.Armor.Increase(Armor);
            }
            else
            {
                throw new ValidationException("target doesn't have armor");
            }
        }
    }
}
