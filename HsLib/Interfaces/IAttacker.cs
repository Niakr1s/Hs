using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLib.Interfaces
{
    public interface IAttacker : ICard, IWithAtk
    {
        /// <summary>
        /// Shows if attacker has some attacks left and it is valid and alive.
        /// </summary>
        /// <param name="bf"></param>
        /// <returns></returns>
        bool CanMeleeAttack(Battlefield bf);

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
