using HsLib.Interfaces;
using HsLib.Types;
using HsLib.Types.Cards;

using HsLib.Types.Events;

namespace HsLib.Systems.Services
{
    public class BattleService
    {
        public BattleService(Battlefield bf)
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
        /// <param name="attackDefender">Is a defender, who takes counterattack. If no defender provided, attacker will try defend by himself.</param>
        /// <returns>True, if attack was actually made.</returns>
        public bool MeleeAttack(IAttacker attacker, IDamageable defender)
        {
            if (!defender.CanBeMeleeAttacked(Bf)) return false;

            if (!attacker.CanMeleeAttack(Bf)) return false;
            Event?.Invoke(this, new BattleMeleePreAttackEventArgs(attacker, defender));
            if (!attacker.CanMeleeAttack(Bf)) return false;

            DealDamage(attacker.Atk.Value, defender);

            if (defender is IAttacker counterAttacker)
            {
                DealDamage(counterAttacker.Atk.Value, attacker.GetDefender(Bf));
            }

            attacker.AfterAttack(Bf);

            Bf.DeathService.ProcessDeaths();

            return true;
        }

        public bool UseEffect(IEffect effect, Pid pid, ICard? target = null)
        {
            bool targetIsValid = effect.EffectType switch
            {
                Types.Effects.EffectType.Self => target is null,
                Types.Effects.EffectType.Mass => target is null,
                Types.Effects.EffectType.Solo => target is not null && effect.UseEffectTargets(Bf, pid).Contains(target),
            };
            if (!targetIsValid)
            {
                return false;
            }

            effect.UseEffect(Bf, pid, target);
            return true;
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