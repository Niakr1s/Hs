using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLib.Types.Effects
{
    public class GetArmorEffect : IEffect
    {
        public int Armor { get; set; }

        public void UseEffect(Battlefield bf, Pid pid, ICard? target)
        {
            bf[pid].Hero.Card.Armor.Increase(2);
        }
    }
}
