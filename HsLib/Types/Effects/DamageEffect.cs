using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLib.Types.Effects
{
    public class DamageEffect : IEffect
    {
        public int Damage { get; set; }

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
