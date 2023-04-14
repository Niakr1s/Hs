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
        /// <returns>True, if attack was actually made.</returns>
        public bool MeleeAttack(IAttacker attacker, IDefender defender, bool isCounterAttack = false)
        {
            if (attacker.Dead) return false;

            if (!isCounterAttack)
            {
                Bf.Invoke(this, new MeleePreAttackEventArgs(attacker, defender));
                if (attacker.Dead) return false;
            }

            int dmg = defender.GetDamage(attacker.Atk.Value);
            Bf.Invoke(this, new GotDamageEventArgs(defender, dmg));

            if (!isCounterAttack && attacker is IDefender cDefender && defender is IAttacker cAttacker)
            {
                MeleeAttack(cAttacker, cDefender, isCounterAttack: true);
            }

            return true;
        }
    }
}
