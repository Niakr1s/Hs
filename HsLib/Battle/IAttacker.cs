using HsLib.Common.Place;
using HsLib.Containers;
using HsLib.Stats;

namespace HsLib.Battle
{
    public interface IAttacker : IWithPlace, IWithAtk, IWithCharge, IWithWindfury
    {
        /// <summary>
        /// Will be called after minion successfully attacked.
        /// </summary>
        /// <param name="bf"></param>
        void AfterAttack(Battlefield bf);
    }
}
