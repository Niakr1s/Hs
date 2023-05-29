using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Stats;

namespace HsLib.Types.Effects
{
    public class DamageEffect : IDamageEffect
    {
        public DamageEffect(int damageAmount)
        {
            DamageAmount = new(damageAmount);
        }

        public IntStat DamageAmount { get; }

        public Action UseEffect(IBoard board, ICard owner, ICard? target)
        {
            if (target is IDamageable d)
            {
                return () => d.Hp.Decrease(DamageAmount);
            }
            else
            {
                throw new ValidationException("target can't be damaged");
            }
        }
    }
}
