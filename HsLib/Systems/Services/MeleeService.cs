using HsLib.Interfaces;

using HsLib.Types.Events;

namespace HsLib.Systems.Services
{
    public class MeleeService
    {
        public MeleeService(Battlefield bf)
        {
            Bf = bf;
        }

        public Battlefield Bf { get; }

        public event EventHandler<BattleEventArgs>? Event;

        /// <summary>
        /// Attacker attacks defender.
        /// </summary>
        /// <param name="attacker">Should have valid place</param>
        /// <param name="defender">Should have valid place.</param>
        /// <returns>True, if attack was actually made.</returns>
        public bool MeleeAttack(IAttacker attacker, IDamageable defender)
        {
            if (!defender.CanBeMeleeAttacked(Bf)) return false;

            if (!attacker.CanMeleeAttack(Bf)) return false;
            Event?.Invoke(this, new BattleMeleePreAttackEventArgs(attacker, defender));
            if (!attacker.CanMeleeAttack(Bf)) return false;

            Bf.BattleService.DealDamage(attacker.Atk.Value, defender);

            if (defender is IAttacker counterAttacker)
            {
                Bf.BattleService.DealDamage(counterAttacker.Atk.Value, attacker.GetDefender(Bf));
            }

            attacker.AfterAttack(Bf);

            Bf.DeathService.ProcessDeaths();

            return true;
        }
    }
}