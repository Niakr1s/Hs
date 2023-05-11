using HsLib.Cards;
using HsLib.Cards.Effects;

namespace HsLib.Battle.Services
{
    public class BattleServiceRules
    {
        public BattleServiceRules(Battlefield bf)
        {
            Bf = bf;
        }

        private Battlefield Bf { get; }

        public bool CanUseEffect(IEffect effect, Card? target = null)
        {
            if (!effect.CanUseEffect(Bf)) return false;

            List<Card> targets = effect.UseEffectTargets(Bf).ToList();

            // nullable to simplify target checks
            if (target is null) { return targets.Count == 0; }
            return targets.Contains(target);
        }
    }
}
