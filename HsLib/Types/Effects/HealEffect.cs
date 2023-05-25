using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Stats;

namespace HsLib.Types.Effects
{
    public class HealEffect : IHealEffect
    {
        public HealEffect(int healAmount)
        {
            Heal = new(healAmount);
        }

        public IntStat Heal { get; }

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
