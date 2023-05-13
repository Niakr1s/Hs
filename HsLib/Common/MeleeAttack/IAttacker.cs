using HsLib.Battle;
using HsLib.Common.Place;

namespace HsLib.Common.MeleeAttack
{
    public interface IAttacker : IWithPlace, IWithAtk
    {
        /// <summary>
        /// Shows if attacker has some attacks left.
        /// </summary>
        /// <param name="bf"></param>
        /// <returns></returns>
        bool CanMeleeAttackThisTurn(Battlefield bf);

        /// <summary>
        /// Will be called after minion successfully attacked.
        /// </summary>
        /// <param name="bf"></param>
        void AfterAttack(Battlefield bf);

        /// <summary>
        /// Returns defender, who will receive counterattack.
        /// </summary>
        /// <param name="bf"></param>
        /// <returns></returns>
        IDamageable GetDefender(Battlefield bf);
    }
}
