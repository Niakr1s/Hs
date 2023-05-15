using HsLib.Interfaces;
using HsLib.Interfaces.CardTraits;
using HsLib.Systems;
using HsLib.Types.Cards;

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
                throw new ArgumentException("target doesn't have armor");
            }
        }
    }
}
