using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Stats;

namespace HsLib.Types.Effects
{
    public class HealEffect : IHealEffect
    {
        public HealEffect(int healAmount)
        {
            HealAmount = new(healAmount);
        }

        public IntStat HealAmount { get; }

        public Action UseEffect(Board board, ICard owner, ICard? target)
        {
            if (target is IDamageable d)
            {
                return () => d.Hp.Increase(HealAmount);
            }
            else
            {
                throw new ValidationException("target can't be healed");
            }
        }
    }
}
