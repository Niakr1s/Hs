using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLib.Types.Effects
{
    public class HealEffect : IHealEffect
    {
        public int Heal { get; set; }

        public Action UseEffect(Battlefield bf, ICard owner, ICard? target)
        {
            if (target is IDamageable d)
            {
                return () => d.Hp.Increase(Heal);
            }
            else
            {
                throw new ValidationException("target can't be healed");
            }
        }
    }
}
