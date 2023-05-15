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
        /// <exception cref="Exception">Throws if can't perform attack</exception>
        /// <returns>Action, that makes attack.</returns>
        public Action MeleeAttack(IAttacker attacker, IDamageable defender)
        {
            if (!defender.CanBeMeleeAttacked(Bf)) { throw new Exception("defender can't be melee attacked"); }
            if (!attacker.CanMeleeAttack(Bf)) throw new Exception("attacker can't attack");
            // checks done

            return () =>
            {
                Event?.Invoke(this, new BattleMeleePreAttackEventArgs(attacker, defender));
                bool canAttack = attacker.CanMeleeAttack(Bf);

                if (attacker.CanMeleeAttack(Bf))
                {
                    Bf.BattleService.DealDamage(attacker.Atk.Value, defender);

                    if (defender is IAttacker counterAttacker)
                    {
                        Bf.BattleService.DealDamage(counterAttacker.Atk.Value, attacker.GetDefender(Bf));
                    }

                    attacker.AfterAttack(Bf);
                }

                Bf.DeathService.ProcessDeaths();
            };
        }

        /// <summary>
        /// Asks minion to attack.
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="defender"></param>
        /// <exception cref="Exception">Throws if can't perform attack</exception>
        /// <returns>Action, that makes attack.</returns>
        public Action MinionAttack(Minion attacker, IDamageable defender)
        {
            if (attacker.Place!.Pid != Bf.Player.Pid) { throw new ArgumentException("wrong attacker turn"); }
            if (attacker.Place.Loc != Loc.Field) { throw new ArgumentException("wrong attacker's location"); }
            if (defender.Place!.Pid != Bf.Enemy.Pid) { throw new ArgumentException("wrong defender turn"); }
            if (defender.Place.Loc != Loc.Field && defender.Place.Loc != Loc.Hero) { throw new ArgumentException("wrong defender location"); }

            if (!attacker.CanMeleeAttack(Bf)) { throw new ArgumentException("attacker can't attack"); }
            if (!defender.CanBeMeleeAttacked(Bf)) { throw new ArgumentException("defender can't be attacked"); }

            return MeleeAttack(attacker, defender);
        }

        /// <summary>
        /// Asks weapon to attack.
        /// </summary>
        /// <param name="defender"></param>
        /// <exception cref="Exception">Throws if can't perform attack</exception>
        /// <returns>Action, that makes attack.</returns>
        public Action WeaponAttack(IDamageable defender)
        {
            if (defender.Place!.Pid == Bf.Turn.Pid) { throw new ArgumentException("wrong defender turn"); }
            return MeleeAttack(Bf.Player.Weapon.Card, defender);
        }
    }
}