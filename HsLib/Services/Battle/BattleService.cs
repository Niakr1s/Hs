using Models.Cards;
using Models.Containers;
using Models.Events;

namespace Models.Services.Battle
{
    public class BattleService
    {
        public BattleService(Battlefield bf)
        {
            Bf = bf;
        }

        public Battlefield Bf { get; }

        /// <summary>
        /// Attacker attacks defender.
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="defender"></param>
        /// <param name="attackDefender">Is a defender, who takes counterattack. If no defender provided, attacker will try defend by himself.</param>
        /// <returns>True, if attack was actually made.</returns>
        private bool MeleeAttack(IAttacker attacker, IDamageable defender,
            IDamageable? attackDefender = null,
            bool isCounterAttack = false)
        {
            attackDefender ??= attacker as IDamageable;
            if (attackDefender?.Dead == true) return false;

            if (!isCounterAttack)
            {
                Bf.Invoke(this, new MeleePreAttackEventArgs(attacker, defender));
                if (attackDefender?.Dead == true) return false;
            }

            int dmg = defender.GetDamage(attacker.Atk.Value);
            Bf.Invoke(this, new GotDamageEventArgs(defender, dmg));

            if (!isCounterAttack && attackDefender is not null && defender is IAttacker counterAttacker)
            {
                MeleeAttack(counterAttacker, attackDefender, isCounterAttack: true);
            }

            return true;
        }

        public bool MinionAttack(Minion attacker, IDamageable defender)
        {
            return MeleeAttack(attacker, defender, attackDefender: attacker);
        }
    }
}
