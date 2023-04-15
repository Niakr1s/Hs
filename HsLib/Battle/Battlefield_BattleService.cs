using HsLib.Events;

namespace HsLib.Battle
{
    public partial class Battlefield
    {
        public class Battlefield_BattleService
        {
            public Battlefield_BattleService(Battlefield bf)
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
            public bool MeleeAttack(IAttacker attacker, IDamageable defender,
                IDamageable? attackDefender = null,
                bool isCounterAttack = false)
            {
                if (Rules?.CanMeleeAttack(attacker, Bf.Turn) == false) return false;
                if (Rules?.CanBeMeleeAttacked(attacker, defender) == false) return false;

                attackDefender ??= attacker as IDamageable;
                if (attackDefender?.Dead == true) return false;
                if (attacker.Atk.Value <= 0) return false;

                if (!isCounterAttack)
                {
                    Bf.Invoke(this, new BattleMeleePreAttackEventArgs(attacker, defender));
                    if (attackDefender?.Dead == true) return false;
                }

                int dmg = defender.GetDamage(attacker.Atk.Value);
                Bf.Invoke(this, new BattleGotDamageEventArgs(defender, dmg));

                if (!isCounterAttack && attackDefender is not null && defender is IAttacker counterAttacker)
                {
                    MeleeAttack(counterAttacker, attackDefender, isCounterAttack: true);
                }

                attacker.AfterAttack(Bf);
                attacker.Atk.AtksThisTurn++;
                return true;
            }
        }
    }
}