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
            Rules = new BattleServiceRules(Bf);
        }

        public Battlefield Bf { get; }

        public BattleServiceRules? Rules { get; set; }

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
            if (Rules?.CanMeleeAttack(attacker, defender) == false) return false;

            attackDefender ??= attacker as IDamageable;
            if (attackDefender?.Dead == true) return false;
            if (attacker.Atk.Value <= 0) return false;

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

        public bool WeaponAttack(Weapon weapon, IDamageable defender)
        {
            bool success = MeleeAttack(weapon, defender);
            if (success)
            {
                weapon.Hp.Decrease();
            }
            return success;
        }
    }
}
