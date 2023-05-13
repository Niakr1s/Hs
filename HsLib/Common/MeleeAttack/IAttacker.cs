using HsLib.Battle;
using HsLib.Common.Place;

namespace HsLib.Common.MeleeAttack
{
    public interface IAttacker : IWithPlace, IWithAtk
    {
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
