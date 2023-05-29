using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLib.Types.Effects
{
    public class GetArmorEffect : IEffect
    {
        public int Armor { get; set; }

        public Action UseEffect(IBoard board, ICard owner, ICard? target)
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
