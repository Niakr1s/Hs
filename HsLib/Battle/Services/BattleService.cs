﻿using HsLib.Cards;
using HsLib.Cards.Effects;
using HsLib.Common.MeleeAttack;
using HsLib.Common.Place;
using HsLib.Events;

namespace HsLib.Battle.Services
{
    public class BattleService
    {
        public BattleService(Battlefield bf)
        {
            Bf = bf;
        }

        public Battlefield Bf { get; }

        public bool WithRules { get; set; } = true;

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
            if (WithRules && !attacker.CanMeleeAttack(Bf)) return false;
            if (WithRules && !defender.CanBeMeleeAttacked(Bf)) return false;

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

            Bf.DeathService.ProcessDeaths();

            return true;
        }

        public bool UseEffect(IEffect effect, Card? target = null)
        {
            if (WithRules && effect.EffectMustHaveTarget && target is null) { return false; }
            if (WithRules && !effect.UseEffectTargets(Bf).Contains(target)) { return false; }
            if (WithRules && !effect.CanUseEffect(Bf)) { return false; }

            effect.UseEffect(Bf, target);
            return true;
        }

        public bool UseAbility(Pid pid, Card? target = null)
        {
            Ability ability = Bf[pid].Ability.Card;
            return UseEffect(ability, target);
        }

        public bool CastSpell(Spell spell, Card? target = null)
        {
            if (!UseEffect(spell, target)) return false;

            Pid spellPid = spell.Pid;
            Bf[spellPid].Hand.Remove(spell);
            Bf[spellPid].Graveyard.Add(spell);
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