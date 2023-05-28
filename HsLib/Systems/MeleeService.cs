using HsLib.Types.Cards;
using HsLib.Types.Places;

namespace HsLib.Systems
{
    public class MeleeService
    {
        public MeleeService(Board board)
        {
            Board = board;
        }

        public Board Board { get; }

        /// <summary>
        /// Attacker attacks defender.
        /// </summary>
        /// <param name="attacker">Should have valid place</param>
        /// <param name="defender">Should have valid place.</param>
        /// <exception cref="Exception">Throws if can't perform attack</exception>
        /// <returns>Action, that makes attack.</returns>
        public Action MeleeAttack(IAttacker attacker, IDamageable defender)
        {
            if (!defender.CanBeMeleeAttacked(Board)) { throw new Exception("defender can't be melee attacked"); }
            if (!attacker.CanMeleeAttack(Board)) throw new Exception("attacker can't attack");
            // checks done

            return () =>
            {
                // todo
                //foreach (ICard card in Board.Cards) { card.OnPreAttack(Board, attacker, defender); }

                // checking again, because attacker can be dead
                bool canAttack = attacker.CanMeleeAttack(Board);

                if (attacker.CanMeleeAttack(Board))
                {
                    defender.Hp.Decrease(attacker.Atk);

                    if (defender is IAttacker counterAttacker)
                    {
                        attacker.GetDefender(Board).Hp.Decrease(counterAttacker.Atk);
                    }

                    attacker.AfterAttack(Board);
                }

                Board.DeathService.ProcessDeaths();
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
            if (attacker.Place.IsNone()) { throw new ArgumentException("attacker place is none"); }
            if (defender.Place.IsNone()) { throw new ArgumentException("defender place is none"); }
            if (attacker.Place.Pid != Board.Player.Pid) { throw new ArgumentException("wrong attacker turn"); }
            if (attacker.Place.Loc != Loc.Field) { throw new ArgumentException("wrong attacker's location"); }
            if (defender.Place.Pid != Board.Enemy.Pid) { throw new ArgumentException("wrong defender turn"); }
            if (defender.Place.Loc != Loc.Field && defender.Place.Loc != Loc.Hero) { throw new ArgumentException("wrong defender location"); }

            if (!attacker.CanMeleeAttack(Board)) { throw new ArgumentException("attacker can't attack"); }
            if (!defender.CanBeMeleeAttacked(Board)) { throw new ArgumentException("defender can't be attacked"); }

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
            if (defender.Place.Pid == Board.Turn.Pid) { throw new ArgumentException("wrong defender turn"); }
            if (Board.Player.Weapon is null)
            {
                return () => { };
            }
            else
            {
                return MeleeAttack(Board.Player.Weapon, defender);
            }
        }
    }
}