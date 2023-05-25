using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Stats;

namespace HsLib.Types.Effects
{
    public class DamageEffect : IDamageEffect
    {
        public DamageEffect(int damageAmount)
        {
            Damage = new(damageAmount);
        }

        public IntStat Damage { get; }

        public Action UseEffect(Battlefield bf, ICard owner, ICard? target)
        {
            if (target is IDamageable d)
            {
                return () => d.Hp.Decrease(Damage);
            }
            else
            {
                throw new ValidationException("target can't be damaged");
            }
        }
    }
}
