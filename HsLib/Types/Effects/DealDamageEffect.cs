using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLib.Types.Effects
{
    public class DealDamageEffect : IEffect
    {
        public int Damage { get; set; }

        public void UseEffect(Battlefield bf, Pid pid, ICard? target)
        {
            if (target is IDamageable d)
            {
                bf.BattleService.DealDamage(Damage, d);
            }
        }
    }
}
