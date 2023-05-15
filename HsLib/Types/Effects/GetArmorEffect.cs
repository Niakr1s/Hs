using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLib.Types.Effects
{
    public class GetArmorEffect : IEffect
    {
        public int Armor { get; set; }

        public void UseEffect(Battlefield bf, ICard? target)
        {
            if (target is IWithArmor a)
            {
                a.Armor.Increase(Armor);
            }
        }
    }
}
