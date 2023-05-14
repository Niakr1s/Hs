using HsLib.Interfaces;
using HsLib.Types;
using HsLib.Types.Cards;
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

        /// <summary>
        /// After variety of checks, attacks. Returns true, if attack was actually done.
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="defender"></param>
        /// <returns>True, if actually attacked. For example, if attacker was killed on pre attack stage, can return false.</returns>
        /// <exception cref="ArgumentException"></exception>
        public bool MinionAttack(Minion attacker, IDamageable defender)
        {
            if (attacker.Place!.Pid != Bf.Player.Pid) { throw new ArgumentException("wrong attacker turn"); }
            if (attacker.Place.Loc != Loc.Field) { throw new ArgumentException("wrong attacker's location"); }
            if (defender.Place!.Pid != Bf.Enemy.Pid) { throw new ArgumentException("wrong defender turn"); }
            if (defender.Place.Loc != Loc.Field && defender.Place.Loc != Loc.Hero) { throw new ArgumentException("wrong defender location"); }

            if (!attacker.CanMeleeAttack(Bf)) { throw new ArgumentException("attacker can't attack"); }
            if (!defender.CanBeMeleeAttacked(Bf)) { throw new ArgumentException("defender can't be attacked"); }

            return MeleeAttack(attacker, defender);
        }

        public bool WeaponAttack(IDamageable defender)
        {
            if (defender.Place!.Pid == Bf.Turn.Pid) { throw new ArgumentException("wrong defender turn"); }
            return MeleeAttack(Bf.Player.Weapon.Card, defender);
        }
    }
}