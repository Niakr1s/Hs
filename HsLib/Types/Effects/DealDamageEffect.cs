using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLib.Types.Effects
{
    public class DealDamageEffect : TargetEffect
    {
        public DealDamageEffect(Card owner, EffectType type, Targets targets) : base(owner, type, targets)
        {
        }

        public int Damage { get; set; }

        protected override void EffectAction(Battlefield bf, ICard? card)
        {
            if (card is IDamageable d)
            {
                bf.BattleService.DealDamage(Damage, d);
            }
        }
    }
}
