using HsLib.Cards;
using HsLib.Cards.Effects;
using HsLib.Common.Interfaces;
using HsLib.Common.Place;
using HsLib.Events;

namespace HsLib.Battle.Services
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

        public event EventHandler<BattleEventArgs>? Event;

        /// <summary>
        /// Attacker attacks defender.
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="defender"></param>
        /// <param name="attackDefender">Is a defender, who takes counterattack. If no defender provided, attacker will try defend by himself.</param>
        /// <returns>True, if attack was actually made.</returns>
        public bool MeleeAttack(IAttacker attacker, IDamageable defender,
            IDamageable? attackDefender = null,
            bool isCounterAttack = false)
        {
            if (Rules?.CanMeleeAttack(attacker, Bf.Turn) == false) return false;
            if (Rules?.CanBeMeleeAttacked(attacker, defender) == false) return false;

            attackDefender ??= attacker as IDamageable;
            //if (attackDefender?.Dead == true) return false; // seems should always counterattack
            if (attacker.Atk.Value <= 0) return false;

            if (!isCounterAttack)
            {
                Event?.Invoke(this, new BattleMeleePreAttackEventArgs(attacker, defender));
                if (attackDefender is IMortal m && m.Dead) return false;
            }

            DealDamage(attacker.Atk.Value, defender);

            if (!isCounterAttack && attackDefender is not null && defender is IAttacker counterAttacker)
            {
                MeleeAttack(counterAttacker, attackDefender, isCounterAttack: true);
            }

            attacker.AfterAttack(Bf);
            attacker.Atk.AtksThisTurn++;

            Bf.DeathService.ProcessDeaths();

            return true;
        }

        public bool UseAbility(Pid pid, Card? target)
        {
            Ability ability = Bf[pid].Ability.Card;
            try
            {
                ability.UseEffect(Bf, ability, target);
                return true;
            }
            catch (EffectWrongTargetException)
            {
                return false;
            }
        }

        /// <summary>
        /// Deals damage and invokes <see cref="BattleGotDamageEventArgs"/>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defender"></param>
        /// <returns>Amount of damage received</returns>
        public int DealDamage(int value, IDamageable defender)
        {
            int dmg = defender.GetDamage(value);
            Event?.Invoke(this, new BattleGotDamageEventArgs(defender, dmg));
            return dmg;
        }
    }
}