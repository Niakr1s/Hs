using HsLib.Stats;
using Models.Common.Place;
using Models.Containers;

namespace Models.Services.Battle
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
