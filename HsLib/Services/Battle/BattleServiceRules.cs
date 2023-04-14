using Models.Cards;
using Models.Common.Place;
using Models.Containers;

namespace Models.Services.Battle
{
    public class BattleServiceRules
    {
        public BattleServiceRules(Battlefield bf)
        {
            Bf = bf;
        }

        private Battlefield Bf { get; }

        private readonly Target _meleeAttackTarget = new Target { Container = TargetContainer.Field | TargetContainer.Hero, Side = TargetSide.He };

        private readonly Loc[] _canAttackFrom = new Loc[] { Loc.Field, Loc.Hero };

        public bool CanBeMeleeAttacked(IWithPlace attacker, IDamageable defender)
        {
            if (!_canAttackFrom.Contains(attacker.Loc)) return false;

            // we don't want attack anything except enemy field or hero
            if (!_meleeAttackTarget.IsValidTarget(attacker, defender)) return false;

            // we don't want attack self
            if (attacker == defender) return false;

            bool defenderFieldHasAnyTaunt = Bf[defender.Pid].Field.Cards.Any(c => c.Taunt.Value && !c.Stealth.Value);

            return defender switch
            {
                Minion m => !m.Stealth.Value && (m.Taunt.Value || !defenderFieldHasAnyTaunt),
                Hero h => !defenderFieldHasAnyTaunt,
                _ => false,
            };
        }
    }
}
