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
    }
}
