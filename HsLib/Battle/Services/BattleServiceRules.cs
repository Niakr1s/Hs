using HsLib.Cards;
using HsLib.Common;
using HsLib.Common.Interfaces;
using HsLib.Common.Place;

namespace HsLib.Battle.Services
{
    public class BattleServiceRules
    {
        public BattleServiceRules(Battlefield bf)
        {
            Bf = bf;
        }

        private Battlefield Bf { get; }

        private readonly Target _meleeAttackTarget =
            new Target { Locs = new() { Loc.Field, Loc.Hero }, Sides = new() { PidSide.He } };

        /// <summary>
        /// Checks if <paramref name="attacker"/> can melee attack <paramref name="defender"/>.
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="defender"></param>
        /// <returns></returns>
        public bool CanBeMeleeAttacked(IWithPlace attacker, IDamageable defender)
        {
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

        private readonly Loc[] _canAttackFrom = new Loc[] { Loc.Field, Loc.Weapon };

        /// <summary>
        /// Checks if <paramref name="attacker"/> can attack this <paramref name="turn"/>.
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="turn"></param>
        /// <returns></returns>
        public bool CanMeleeAttack(IAttacker attacker, Turn turn)
        {
            if (attacker.Atk == 0) { return false; }
            if (!_canAttackFrom.Contains(attacker.Loc)) { return false; }

            if (attacker.Pid == Pid.None || attacker.Pid != turn.Pid) { return false; }

            int attacksAllowed = attacker.Windfury.Value ? 2 : 1;
            int attacksLeft = attacksAllowed - attacker.Atk.AtksThisTurn;

            if (attacksLeft <= 0) { return false; }

            bool sameTurn = attacker.TurnAdded == turn.No;

            return !sameTurn || attacker.Charge;
        }
    }
}
